﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using JetBrains.Annotations;
using Selkie.Aop.Messages;
using Selkie.EasyNetQ;
using Selkie.Services.Aco.Common.Messages;
using Selkie.Services.Common;
using Selkie.Windsor.Extensions;

namespace Selkie.Services.Aco.Console.Client
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class AcoServiceClient : IAcoServiceClient
    {
        private const int SleepTimeOneSecond = 1000;
        private readonly ISelkieBus m_Bus;
        private readonly ISelkieConsole m_Console;

        private readonly int[][] m_CostMatrix =
        {
            new[]
            {
                1,
                2,
                3,
                4
            },
            new[]
            {
                5,
                6,
                7,
                8
            },
            new[]
            {
                9,
                10,
                11,
                12
            },
            new[]
            {
                13,
                14,
                15,
                16
            }
        };

        private readonly int[] m_CostPerLine =
        {
            1000,
            1,
            1000,
            1
        };

        private bool m_IsReceivedCreatedColonyMessage;
        private bool m_IsReceivedFinishedMessage;
        private bool m_IsReceivedStartedyMessage;

        public AcoServiceClient([NotNull] ISelkieBus bus,
                                [NotNull] ISelkieConsole console)
        {
            m_Bus = bus;
            m_Console = console;

            string subscriptionId = GetType().ToString();

            m_Bus.SubscribeAsync <CreatedColonyMessage>(subscriptionId,
                                                        CreatedColonyHandler);

            m_Bus.SubscribeAsync <StartedMessage>(subscriptionId,
                                                  StartedHandler);

            m_Bus.SubscribeAsync <BestTrailMessage>(subscriptionId,
                                                    BestTrailHandler);

            m_Bus.SubscribeAsync <FinishedMessage>(subscriptionId,
                                                   FinishedHandler);

            m_Bus.SubscribeAsync <ExceptionThrownMessage>(subscriptionId,
                                                          ExceptionThrownHandler);
        }

        public void CreateColony()
        {
            m_Console.WriteLine("Request <CreateColonyMessage>...");

            var request = new CreateColonyMessage
                          {
                              CostMatrix = m_CostMatrix,
                              CostPerLine = m_CostPerLine
                          };

            m_Bus.Publish(request);

            WaitForCreatedMessage();
        }

        private void FinishedHandler(FinishedMessage message)
        {
            DisplayReceivedMessage(message);

            m_IsReceivedFinishedMessage = true;
        }

        private void BestTrailHandler([NotNull] BestTrailMessage message)
        {
            DisplayReceivedMessage(message);

            string trailText = string.Join(",",
                                           message.Trail);

            m_Console.WriteLine("Iteration {0}: Length = {1} Trail = {2}".Inject(message.Iteration,
                                                                                 message.Length,
                                                                                 trailText));
        }

        private void StartedHandler([NotNull] StartedMessage message)
        {
            DisplayReceivedMessage(message);

            m_IsReceivedStartedyMessage = true;
        }

        private void WaitForCreatedMessage()
        {
            SleepWaitAndDo(() => m_IsReceivedCreatedColonyMessage,
                           () => m_Console.WriteLine("Waiting for response 'CreatedColonyMessage'..."));
        }

        private void SleepWaitAndDo([NotNull] Func <bool> breakIfTrue,
                                    [NotNull] Action doSomething)
        {
            for ( var i = 0 ; i < 10 ; i++ )
            {
                Thread.Sleep(SleepTimeOneSecond);

                if ( breakIfTrue() )
                {
                    break;
                }

                doSomething();
            }
        }

        public void WaitForFinishColony()
        {
            m_Console.WriteLine("Wait for <FinishMessage>...");

            WaitForFinishMessage();

            m_Console.WriteLine("...received <FinishMessage>!");
        }

        private void WaitForFinishMessage()
        {
            SleepWaitAndDo(() => m_IsReceivedFinishedMessage,
                           () => m_Console.WriteLine("Waiting for response 'FinishMessage'..."));
        }

        public void StartColony()
        {
            m_Console.WriteLine("Request <StartMessage>...");

            var request = new StartMessage
                          {
                              Times = 10
                          };

            m_Bus.Publish(request);

            WaitForStartedMessage();
        }

        private void WaitForStartedMessage()
        {
            SleepWaitAndDo(() => m_IsReceivedStartedyMessage,
                           () => m_Console.WriteLine("Waiting for response 'StartedMessage'..."));
        }

        private void CreatedColonyHandler([NotNull] CreatedColonyMessage message)
        {
            DisplayReceivedMessage(message);

            m_IsReceivedCreatedColonyMessage = true;
        }

        private void DisplayReceivedMessage(object message)
        {
            m_Console.WriteLine("Received {0}...".Inject(message.GetType().Name));
        }

        private void ExceptionThrownHandler(ExceptionThrownMessage message)
        {
            m_Console.WriteLine(MessageToText(message)); // todo write a ExceptionThrownMessage handler in WPF solution
        }

        private string MessageToText(ExceptionThrownMessage message)
        {
            var builder = new StringBuilder();

            builder.AppendLine("Invocation: {0}".Inject(message.Invocation));
            builder.AppendLine("Message: {0}".Inject(message.Message));
            builder.AppendLine("StackTrace: {0}".Inject(message.StackTrace));

            return builder.ToString();
        }

        public void ForceException()
        {
            m_Console.WriteLine("ForceException <CreateColonyMessage>...");

            var request = new CreateColonyMessage
                          {
                              CostMatrix = new[]
                                           {
                                               new[]
                                               {
                                                   1,
                                                   2
                                               }
                                           },
                              CostPerLine = new[]
                                            {
                                                1,
                                                2,
                                                3,
                                                4
                                            }
                          };

            m_Bus.Publish(request);
        }
    }
}