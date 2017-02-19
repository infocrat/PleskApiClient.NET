# Plesk API C# client

### Start guide
1. Create client object
PleskApiClient client = new PleskApiClient("host.com", "admin", "password");

2. Use API endpoints

Now it supports pure functionality:
 - Customers
 - Subscriptions

Customer customer = new Customer();
client.Customers.Add(customer);
