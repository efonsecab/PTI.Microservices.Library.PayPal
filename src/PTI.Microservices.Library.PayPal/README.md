# PTI.Microservices.Library.Paypal

Facilitates the consumption of the APIs in Paypal

**Examples:**

**Note: The examples below are passing null for the logger, if you want to use the logger make sure to pass the parameter with a value other than null**

## Create Batch Payout
    PaypalService paypalService =
                                new PaypalService(null, this.PaypalConfiguration,
                                customHttpClient: new CustomHttpClient(new CustomHttpClientHandler(null)));
    var accessTokenResult = await paypalService.GetAccessTokenAsync(null);
    var requestModel = new CreateBatchPayoutRequest()
    {
        sender_batch_header  = new Sender_Batch_Header()
        {
            email_message = "This is a test payment",
            email_subject= " Test Subject from PTI Microservices Library",
            sender_batch_id = $"Sender Batch Id: {Guid.NewGuid()}"
        },
        items = new Item[] 
        {
            new Item()
            {
                amount = new Amount()
                {
                    currency = "USD",
                    value = "1.5",
                },
                note = "This is a test payment note",
                notification_language = "en-US",
                receiver = RECEIVER_EMAIL,
                recipient_type = "EMAIL",
                sender_item_id = $"Sender Item Id: {Guid.NewGuid()}",
            }
        }
    };
    var result = await paypalService.CreateBatchPayoutAsync(requestModel,
        accessTokenResult.access_token, CancellationToken.None);
    Assert.IsNotNull(result);
    var result2 = await paypalService.GetPayoutBatchDetailsAsync(result.batch_header.payout_batch_id,
        accessTokenResult.access_token, CancellationToken.None);
    Assert.IsNotNull(result2);

## Get Order Details
    PaypalService paypalService =
                    new PaypalService(null, this.PaypalConfiguration,
                    customHttpClient: new CustomHttpClient(new CustomHttpClientHandler(null)));
    var accessTokenResult = await paypalService.GetAccessTokenAsync(null);
    string orderId = TEST_ORDER_ID;
    var orderInfo = await paypalService.GetOrderDetailsAsync(orderId, accessTokenResult.access_token);

## List Products
    PaypalService paypalService =
        new PaypalService(null, this.PaypalConfiguration,
        customHttpClient: new CustomHttpClient(new CustomHttpClientHandler(null)));
    var accessTokenResult = await paypalService.GetAccessTokenAsync(null);
    var result = await paypalService.ListProductsAsync(accessTokenResult.access_token);

## List Plans
    PaypalService paypalService =
        new PaypalService(null, this.PaypalConfiguration,
        customHttpClient: new CustomHttpClient(new CustomHttpClientHandler(null)));
    var accessTokenResult = await paypalService.GetAccessTokenAsync(null);
    var productsResult = await paypalService.ListProductsAsync(accessTokenResult.access_token);
    var firstPlan = productsResult.products.First();
    await paypalService.ListPlansAsync(accessTokenResult.access_token, firstPlan.id);

## Get Subscription Details
    PaypalService paypalService =
        new PaypalService(null, this.PaypalConfiguration,
        customHttpClient: new CustomHttpClient(new CustomHttpClientHandler(null)));
    var accessTokenResult = await paypalService.GetAccessTokenAsync(null);
    var result =
    await paypalService.GetSubscriptionDetailsAsync(accessTokenResult.access_token, this.TestPaypalSubscriptionId);