# Introduction
Shoppe is a simple shopping app.

# Solution Overview
There are two components to this app: `shoppe-ui` and `shoppe-api`.

`shoppe-ui` is a regular SPA that uses following,
- `Bootstrap` to create a responsive UI
- [Observable Data Service](https://blog.angular-university.io/how-to-build-angular2-apps-using-rxjs-observable-data-services-pitfalls-to-avoid/) pattern as the state management solution
- `jasmine` for unit testing

`shoppe-api` is a .NET Core Web Api that uses following,
- `InMemory Cache` as data store
- `FluentValidation` for request validation
- `Swagger` for documentation
- `ExceptionMiddleWare` for handling unhandled exceptions
- `Moq` for dependency mocking in unit tests

# Verifying Deployed Solution
This application is hosted in Azure and can be verified by visiting the [shoppe app](https://shoppestg.z8.web.core.windows.net/).

# Setting up locally
- Clone the [repo](https://github.com/martinmthomas/shoppe).
- Navigate to "shoppe-api", open the solution in Visual Studio and hit `F5`.
- Open Terminal
- Navigate to "shoppe-ui" and run the following commands,
```
npm install
npm run start
```
*Note that the url must be `http://localhost:4200` for Cors setup allows only this origin.*