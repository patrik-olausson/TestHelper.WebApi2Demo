Feature: GetValues
	As a developer
	In order to show how specflow works	
	I want to get two values from the values controller


Scenario: Successfully retreiving values
	Given there are two values
	When I get the values
	Then I should get an array with the two values
