# Plesk API C# client

#### Project website: <a target="_blank" href="https://infocrat.github.io/PleskApiClient.NET/">https://infocrat.github.io/PleskApiClient.NET/</a>
### Start guide
1. Create client object
PleskApiClient 
```
client = new PleskApiClient("host.com", "admin", "password");
```

2. Use API endpoints

Now it supports pure functionality:
 - Customers
 - Subscriptions
```
Customer customer = new Customer();
client.Customers.Add(customer);
```
