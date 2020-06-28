Feature: GoogleSearch
	In order to see search results for 'Google'
	As a user
	I want to search 'Google' in google search option

@google
Scenario: See search results for 'Google'
	Given I've launched chrome browser
	And I'm in google home page
	When I enter Google in search bar and click on Google Search button
#	Then I can see search results


Scenario: Open first link in search resutls
	Given I'm seeing search results for 'Google'
	When I click on first search results link
	Then I can see new page is displayed