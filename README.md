# Expense exercise for Careebiz 

1. Prerequisites
   a. .NET Core SDK 3.0
   b. Visual Studio 2019 / Visual Studio Code

2. Expense API quick reference:

            API	                                                  |   Description	                               |  Request body	|         Response body
a. GET  https://localhost:5001/api/Expense/	                      |   Get all expenses	                         |  None	        |         Array of expenses
b. GET  https://localhost:5001/api/Expense/{id}	                  |   Get expense by ID	                         |  None	        |         Expense or Not found.
c. POST https://localhost:5001/api/Expense	                      |   Add a new expense object                   |	Expense item  |         new expense
d. POST https://localhost:5001/api/Expense/AddEuroToPoundExpense  |   Add a new expense with EUR/GBP conversion  |	none          |         new expense

   
   
   
   
