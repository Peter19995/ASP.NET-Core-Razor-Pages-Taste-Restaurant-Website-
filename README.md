
# Taste Restaurant Website (ASP.NET-Core-Razor-Pages)

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## Description

Taste Restaurant Website is an intermediate to advanced level ASP.NET Core (.NET 7) project that focuses on building a real-world application using Razor Pages. This project is designed for individuals who have a solid understanding of ASP.NET Core basics and are interested in learning how to architect and develop professional-grade applications.

The website provides an interactive platform for customers to explore food items, add them to their cart, and place orders using a secure credit card payment system. Administrators have access to order details, allowing them to track orders from start to completion. They can also manage orders by providing options for cancellation and refunds.

## Key Features

- Structure and organization of an ASP.NET Core (.NET 7) project
- Implementing basic security measures in ASP.NET Core applications
- Building applications with ASP.NET Core (.NET 7) using the Model-View-Controller (MVC) pattern
- Implementing the Repository Pattern for efficient data management
- Applying N-Tier architecture principles for scalable and maintainable code
- Integrating Stripe for payment processing and refunds
- Utilizing Datatables API Calls with Razor Pages for enhanced data presentation
- Integrating Identity Framework and extending user fields
- Incorporating Entity Framework and leveraging code-first migrations
- Implementing authentication and authorization in ASP.NET Core (.NET 7)
- Managing user sessions in ASP.NET Core (.NET 7)
- Data seeding for initial data population and deployment to Azure

## Getting Started

To run the Taste Restaurant Website project locally, follow these steps:

1. Clone the repository:
   ```
   git clone https://github.com/Peter19995/ASP.NET-Core-Razor-Pages.git
   ```

2. Open the project in your preferred development environment (e.g., Visual Studio).

3. Configure the necessary dependencies and packages, ensuring you have the appropriate versions of ASP.NET Core and related libraries.

4. Set up required database connections and update connection strings.

5. Set up facebook Application ID and Key plus stripe Publishable Key and SecretKey.

6. Under Utility project, make sure to change your email and Password.
    ```
        emailClient.Authenticate("your_email", "password");
    ```

7. Build the project and resolve any compilation errors.

8. Run the application and access it through your web browser.

## Usage

Upon accessing the Taste Restaurant Website, customers can browse the available food items, add them to their cart, and proceed to the checkout page to place their orders securely using a credit card. Administrators can log in to access the order management interface, where they can view, track, and manage orders.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

We would like to express our gratitude to the instructors and creators of the [Intermediate to Advance level ASP.NET Core (.NET 6)](https://www.udemy.com/course/advanced-aspnet-core-3-razor-pages/?referralCode=6C89600F2C73A16F63F3) course that served as the foundation for this project. Their expertise and dedication to providing clear and concise explanations have been invaluable in building this real-world application.

## Contact Information

For any inquiries or feedback, please email [osodoptah1995@gmail.com].