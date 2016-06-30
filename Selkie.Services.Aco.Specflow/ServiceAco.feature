@LongRunningTest
Feature: ServiceAco

Scenario: Ping AcoService
	Given Service is running
	And Did not receive ping response message
	When I send a ping message
	Then the result should be a ping response message

Scenario: Stop AcoService
	Given Service is running
	And Did not receive ping response message
	When I send a stop message
	Then the result should be service not running

Scenario: Stopping AcoService sends message
	Given Service is running
	When I send a stop message
	Then the result should be that I received a ServiceStoppedMessage

Scenario: Started AcoService sends message
	Given Service is running
	Then the result should be that I received a ServiceStartedMessage

Scenario: CreateColony message request and response
	Given Service is running
	And Did not a receive a CreatedColonyMessage
	When I send a CreateColonyMessage
	Then the result should be that I received a CreatedColonyMessage

Scenario: Start colony message request and response
	Given Service is running and colony created
	And Did not a receive a StartedMessage
	When I send a StartMessage
	Then the result should be that I received a StartedMessage

Scenario: Colony sends BestTrailMessage
	Given Service is running and colony created
	And Did not a receive a BestTrailMessage
	Then the result should be that I received a BestTrailMessage

Scenario: Colony sends FinishedMessage
	Given Service is running and colony created
	And Did not a receive a FinishedMessage
	Then the result should be that I received a FinishedMessage

