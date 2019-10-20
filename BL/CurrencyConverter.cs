using System;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Options;

namespace ExpenseApi.BL
{
    public class CurrencyConverter   
    {
      private readonly string _apiUrl;
      public CurrencyConverter(string apiUrl)
      {
          _apiUrl = apiUrl;
      }
      public async Task<decimal?> EuroToPound(string amount)
      {
        decimal? amountInEur  = TryExtractAmount(amount, "EUR");
        if(amountInEur.HasValue)
          using(HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(_apiUrl))
              using (HttpContent content = res.Content)
              {
                var data = await content.ReadAsStringAsync();
                if (data != null)
                {
                  var dataObj = JObject.Parse(data);
                  if(decimal.TryParse(dataObj["rates"]["GBP"].ToString(), out decimal conversionRate))
                  {
                    return conversionRate * amountInEur;
                  }
                }
              }
        return null;
      }

      private decimal? TryExtractAmount(string amountStr, string currency)
      {
        if(!string.IsNullOrWhiteSpace(amountStr))   
        {
          if(amountStr.Contains(currency, StringComparison.CurrentCultureIgnoreCase))
          {
            string [] amountArr = amountStr.ToUpper().Split(currency, StringSplitOptions.RemoveEmptyEntries);
            if(amountArr.Length ==1)
            {
              decimal amount;
              if(decimal.TryParse(amountArr[0], out amount))
              {
                return amount;
              }
            }
          }
        }
        return null;
      }
    }
  }

