Feature: Create a new order

  Scenario: Successfully create a food order
    Given I load the request payload for "order_001"
    When I send a POST request to CreateOrder API with headers
    Then the response code should be 201
    And the response should match expected "order_001"