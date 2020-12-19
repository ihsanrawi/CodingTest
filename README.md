# YouLend Coding Test

## **Steps to run servers**

### In `Website` folder

1. Run `yarn` or `npm` to download node_modules dependencies.
2. Then run `yarn start` or `npm start` to run the React server.
3. Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

### In `Web Api` folder

1. Run `dotnet build` to build the Api.
2. Run `dotnet run` to run the api.
3. Open [https://localhost:5001/swagger](https://localhost:5001/swagger) to view available endpoints using Swagger.

## **React Client**

The front end is developed using `React` and styled using `Bootstrap`. I used `Axios` to interact with the API.

I used some React hooks such as `useState`, `useEffect` to store state and fetch api data.

There's 3 components which are for search, add and list loan items.
Delete method is placed in the list loan items itself.

## **API**

The API is developed using `SOLID principles` in mind.

I've added `Swagger` for API documentation and endpoints testing.

There's seed data in the in memory database to test retrieve loans.

I've configured the repayment amount to be 20% more than funding amount. its calculated both in front-end and back-end.

I had utilize soft delete instead of hard delete by making Deleted value to be true in the Loan model.

There's 2 models which are:

1. NewLoan - View model as data from front-end doesn't have certain props in the entity model.
2. Loan - Entity model.

### **Unit Test**

I've created some simple unit test for all the layers. There's still improvement can be made as a lot of other test cases is not cater.
