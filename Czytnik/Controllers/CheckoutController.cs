using System;
using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Stripe;

namespace Czytnik.Controllers
{
  public class CheckoutController : Controller
  {
    public IActionResult Index()
    {
      StripeConfiguration.ApiKey = "sk_test_51KPASKGvoTqJgR6RzAxqrWRehxLko3wYbsphwxrzUmi8tgRsRxtOLm20B9LWgHXBfFLqM2uIBxKnwwqJI8amTD1t00Ww8W9c02";
      return View();
    }

    [Route("checkout-session")]
    [HttpPost]
    public IActionResult CheckoutSession(PaymentIntentCreateRequest request)
    {
      Console.WriteLine("-----------------------------");
      var paymentIntentService = new PaymentIntentService();
      var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
      {
        Amount = CalculateOrderAmount(request.Items),
        Currency = "pln",
        AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
        {
          Enabled = true,
        },
      });

      return Json(new { clientSecret = paymentIntent.ClientSecret });
    }

    private int CalculateOrderAmount(Item[] items)
    {
      // Replace this constant with a calculation of the order's amount
      // Calculate the order total on the server to prevent
      // people from directly manipulating the amount on the client
      return 1400;
    }

    public class Item
    {
      [JsonProperty("id")]
      public string Id { get; set; }
    }

    public class PaymentIntentCreateRequest
    {
      [JsonProperty("items")]
      public Item[] Items { get; set; }
    }
  }
}
