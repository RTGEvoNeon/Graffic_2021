using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practice.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using static Practice.Models.Graffic;

namespace Practice.Controllers
{
    public class GrafficController : Controller
    {
        public Rootobject makeModels()
        {
            string result;
            var url = "https://api.planfact.io/api/v1/bizinfos/accountshistory";

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Accept = "application/json";
            httpRequest.Headers["X-ApiKey"] = "wvxxRpxaUbIS8BN9gmvWMN2U8Iv8c1BbQTaCRb9LguX4OUVMlK-Ex9SttOUVgRxi-Mfz8QZOf81cHwhj47YLmvz1C8qHEvKkH9DUzhzDdy8GJ51wKLO2MlN8032bQ7tpI4YtkgHSWxbMb2lkMXM4KkWQOYyP9VZAgM4CCdbWAj5uhxkuL2XVcZy6K0oGJOaD4P9-3WL5v2A4xhdK_IpYwJxl1Kz5IDP9gBhZmwwggr8yQT5nwzOItGmUgn3hcx10oqKtxiPWYU9I31KCyb8blLsXIlvG-_Yvi9uB7YTggmGyZsqoyQNAEh-zeDKj9gbeSRBIf5gTXabWn-GSGYOzt5p1bDgubmxurHIyiWZHyZw9hx9MVjDT-8V_D9yXYDt9LkisRKp9x9kMK7zMivTbemsRqFw5yMB7CFhyrwQWkk2vChk5V6nO1e4LjiJ-MvZ4D6bvcSaKPNXwIkcYD8NcAT5TAERGGaHJT9j4pvchfPmPXsefxQbvBNtbW93h01ivKCy0p6gbW0vb5VV0kEYgWblAU1OzfKRENYzWZbXeAoXG3npoK0nyqEaFGcgvRXpKwz6oBtFYUgHe93_Fbk9sOSRO1pIXI7T3GPraoWuBAs4NAEL1";


            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            Rootobject m = JsonConvert.DeserializeObject<Rootobject>(result);

            return m;
        }

        public string DataForGraffic(Rootobject model)
        {
            DateTime begin = DateTime.Today;
            DateTime end = DateTime.Today;
            
            for (int i = 0; i < model.data.Length; i++)
            {
                DateTime parse_date = model.data[i].details[0].date;
                if (model.data[i].details[0].date < begin)
                    begin = parse_date;

                int size = model.data[i].details.Length;
                if (model.data[i].details[size-1].date > end)
                {
                    end = model.data[i].details[size - 1].date;
                }
            }
           
            int days = (end - begin).Days;
            Total[] totals = new Total[days];
            DateTime temp_time = begin;

            for (int k = 0; k < days; k++)
            {
                if (k == 0)
                {
                    totals[k] = new Total();
                    totals[k].date = begin;
                }
                else
                    totals[k] = new Total(totals[k-1]);

                for (int i = 0; i < model.data.Length; i++)
                {
                    for (int j = 0; j < model.data[i].details.Length; j++)
                    {
                        double sum = 0;
                        if (model.data[i].details[j].date == totals[k].date)
                            if (totals[k].date < DateTime.Today)
                                sum = model.data[i].details[j].factValue;
                            else
                                sum = model.data[i].details[j].planValue;
                        switch (i)
                        {
                            case 0:
                                totals[k].Sberbank += sum;   
                                break;
                            case 1:
                                totals[k].Tinkoff += sum;
                                break;
                            case 2:
                                totals[k].CentralBank += sum;
                                break;
                            case 3:
                                totals[k].Cash += sum;
                                break;
                            default: return null;
                        }
                        totals[k].allMoney += sum;
                    }
                }
            }
            string json = JsonConvert.SerializeObject(totals);
            return json;
        }

        public IActionResult Graffic()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var model = makeModels();
            var result = DataForGraffic(model);

            stopwatch.Stop();
            long times = stopwatch.ElapsedMilliseconds;
            ViewData["Time"] = times;


            ViewBag.Myjson = result;
            return View();
        }
        [HttpPost]
        public IActionResult Graffic(string date1, string date2)
        {
            string range = $"Date1: {date1} Date2: {date2}";
            ViewBag.Myjson = DataForGraffic(makeModels(), date1, date2);
            return View();
        }
        public string DataForGraffic(Rootobject model, string date1, string date2)
        {
            DateTime begin = DateTime.Parse(date1);
            DateTime end = DateTime.Parse(date2);

            int days = (end - begin).Days;
            Total[] totals = new Total[days];
            DateTime temp_time = begin;

            for (int k = 0; k < days; k++)
            {
                if (k == 0)
                {
                    totals[k] = new Total();
                    totals[k].date = begin;
                }
                else
                    totals[k] = new Total(totals[k - 1]);

                for (int i = 0; i < model.data.Length; i++)
                {
                    for (int j = 0; j < model.data[i].details.Length; j++)
                    {
                        double sum = 0;
                        if (model.data[i].details[j].date == totals[k].date)
                            if (totals[k].date < DateTime.Today)
                                sum = model.data[i].details[j].factValue;
                            else
                                sum = model.data[i].details[j].planValue;
                        switch (i)
                        {
                            case 0:
                                totals[k].Sberbank += sum;
                                break;
                            case 1:
                                totals[k].Tinkoff += sum;
                                break;
                            case 2:
                                totals[k].CentralBank += sum;
                                break;
                            case 3:
                                totals[k].Cash += sum;
                                break;
                            default: return null;
                        }
                        totals[k].allMoney += sum;
                    }
                }
            }
            string json = JsonConvert.SerializeObject(totals);
            return json;
        }
    }
}
