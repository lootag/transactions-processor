# Transactions Processor
This is a small application that returns security transactions data, enriched with some information regarding the legal entities involved in those transactions.

## Background, Project Structure, and Motivations
Quite frankly, I initially thought of structuring this project as a CLI, ideally something that looked like this `$> txprocessor --input-path=/path/to/input/file --output-path=/path/to/output/file`. However, I had a very good discussion with Serkan, where some things popped up that I didn't quite expect:
- The users are way more technical than what I initially thought: they know how to consume web apis, and can process the output according to their needs;
- The users would consume my software through some clis scheduled by Apache Airflow, and that would therefore run in the cloud (and not on their machines).
- The users don't actually have the csv file attached to the assignment: that data comes from an external source (an HTTP endpoint I would presume). From my conversation with Serkan, I understood that this external source returns the latest transaction data. As Serkan rightfully pointed out, if they had the csv, they wouldn't need my software at all.
- This external source might return stale data, and therefore the users might have to run my software more than once.

Because of these reasons, I've decided to structure my project as a Web Api that returns the data as JSON (because web frameworks are way better at processing JSON than csv, and furthermore this also makes the api more general, in case some other client wanted to consume it). The private method `RetrieveRawTransactions` in the `SecurityTransactionService` class simulates the call to the external source that returns the data in the csv (for the purpouse of this assignment it simply reads the csv from the file system). I've used Polly to introduce a retry policy for the call to the GLEIF api, as Serkan told me that sometimes there might be a glitch.

## Depoloyed Version
I've containerized the application, and I've deployed it to an Azure App Service. The url is  https://transactionsprocessor.azurewebsites.net/api/v1/security-transactions. In order to get the JSON result, just curl the url. Alternatively you can use postman, or simply put the url in your browser tab.

## Build and run locally
### With Docker
If you want to build the application with Docker, just run `$> docker build -t transactionsprocessor:latest .` from the root of the project. You can then start the container with `$> docker run -p 80:80 transactionsprocessor:latest`. All you need to do at that point is curl localhost:80/api/v1/security-transactions.

### Without Docker
If you don't want to use Docker, you're going to need the .NET 6 sdk, and the .NET 6 runtime installed on your machine. Run `$> dotnet publish -c Release -o target` in order to build the application, and then `$> dotnet Lootag.TransactionsProcessor.WebApi.dll` in order to run it.

## Test Suite
In order to run the tests you're going to need the .NET 6 sdk, and the .NET 6 runtime installed on your machine.
### Unit Tests
I've written a couple of unit tests for the computation of transaction costs. Just cd into `Lootag.TransactionsProcessor.UnitTests` and run `$> dotnet test`.
