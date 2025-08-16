Feature: Order Management
  Scenario: Create and Pay for an Order
    Given I have a new customer
    When I create an order for the customer
    And I add an item "Laptop" costing 1200
    And I pay the order
    Then the order should be marked as paid
