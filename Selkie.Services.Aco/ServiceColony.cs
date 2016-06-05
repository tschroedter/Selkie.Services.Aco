using System;
using Castle.Core;
using JetBrains.Annotations;
using Selkie.Aco.Anthill;
using Selkie.Aco.Anthill.Interfaces;
using Selkie.Aco.Anthill.TypedFactories;
using Selkie.Aco.Common.Interfaces;
using Selkie.Aop.Aspects;
using Selkie.Common.Interfaces;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Windsor;

namespace Selkie.Services.Aco
{
    [Interceptor(typeof( MessageHandlerAspect ))]
    [ProjectComponent(Lifestyle.Transient)]
    public sealed class ServiceColony
        : IServiceColony,
          IDisposable
    {
        public ServiceColony([NotNull] IDisposer disposer,
                             [NotNull] ISelkieBus bus,
                             [NotNull] IColonyFactory colonyFactory,
                             [NotNull] IDistanceGraphFactory graphFactory,
                             [NotNull] IAntSettingsFactory antSettingsFactory,
                             [NotNull] IServiceColonyParameters parameters)
        {
            m_Disposer = disposer;
            m_Bus = bus;

            IDistanceGraph graph = graphFactory.Create(parameters.CostMatrix,
                                                       parameters.CostPerFeature);

            IAntSettings antSettings = antSettingsFactory.Create(parameters.IsFixedStartNode,
                                                                 parameters.FixedStartNode);

            IColony colony = colonyFactory.Create(graph,
                                                  antSettings);

            m_Colony = colony;

            RegisterEventHandlers();
        }

        internal IColony Colony
        {
            get
            {
                return m_Colony;
            }
        }

        private readonly ISelkieBus m_Bus;
        private readonly IColony m_Colony;
        private readonly IDisposer m_Disposer;

        public void Dispose()
        {
            if ( m_Disposer.IsDisposed )
            {
                return;
            }

            m_Disposer.Dispose();
        }

        public bool IsRunning { get; set; }

        public double PheromonesMinimum
        {
            get
            {
                return m_Colony.PheromonesMinimum;
            }
        }

        public double PheromonesMaximum
        {
            get
            {
                return m_Colony.PheromonesMaximum;
            }
        }

        public double PheromonesAverage
        {
            get
            {
                return m_Colony.PheromonesAverage;
            }
        }

        public double[][] PheromonesToArray()
        {
            return m_Colony.PheromonesToArray();
        }

        public void Stop()
        {
            m_Colony.Stop();
        }

        public void Start(int times)
        {
            m_Colony.Start(times);
        }

        internal void OnBestTrailChanged(object sender,
                                         BestTrailChangedEventArgs e)
        {
            var message = new BestTrailMessage
                          {
                              Alpha = e.Alpha,
                              Beta = e.Beta,
                              Gamma = e.Gamma,
                              Iteration = e.Iteration,
                              Length = e.Length,
                              Trail = e.Trail,
                              Type = e.AntType
                          };

            m_Bus.PublishAsync(message);
        }

        internal void OnFinished(object sender,
                                 FinishedEventArgs e)
        {
            var message = new FinishedMessage
                          {
                              FinishTime = e.FinishTime,
                              StartTime = e.StartTime,
                              Times = e.Times
                          };

            m_Bus.PublishAsync(message);

            IsRunning = false;
        }

        internal void OnStarted(object sender,
                                EventArgs e)
        {
            var message = new StartedMessage();

            m_Bus.PublishAsync(message);

            IsRunning = true;
        }

        internal void OnStopped(object sender,
                                EventArgs e)
        {
            var message = new StoppedMessage();

            m_Bus.PublishAsync(message);

            IsRunning = false;
        }

        private void RegisterEventHandlers()
        {
            m_Colony.BestTrailChanged += OnBestTrailChanged;
            m_Disposer.AddResource(() => m_Colony.BestTrailChanged -= OnBestTrailChanged);

            m_Colony.Finished += OnFinished;
            m_Disposer.AddResource(() => m_Colony.Finished -= OnFinished);

            m_Colony.Stopped += OnStopped;
            m_Disposer.AddResource(() => m_Colony.Stopped -= OnStopped);

            m_Colony.Started += OnStarted;
            m_Disposer.AddResource(() => m_Colony.Started -= OnStarted);
        }
    }
}