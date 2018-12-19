using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AssessmentManagerAPI.Controllers.Model;

namespace AssessmentManagerAPI.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        public string strFile = Common.Common.APIDataFolder + "productData.js";

        /*  [System.Web.Http.AcceptVerbs("GET", "POST")]
          [System.Web.Http.HttpPost]
          [Route("saveProduct")]
          public Product saveProduct(Product Product)
          {
              List<Product> _data = getData();

              if (_data == null) _data = new List<Product>();


              //  Product.Id = "999";


              if (Product != null)
              {
                  if (Product.Id != 0)
                  {
                      var tempQ = _data.FirstOrDefault(r => r.Id == student.Id);
                      tempQ.Name = student.Name;
                      tempQ.Address = student.Address;
                      tempQ.City = student.City;
                      tempQ.State = student.State;
                      tempQ.Zip = student.Zip;
                      tempQ.Assessments = student.Assessments;
                  }
                  else
                  {
                      if (_data.Count == 0)
                          student.Id = 1;
                      else
                          student.Id = (Convert.ToInt32(_data.Max(r => r.Id)) + 1);

                      _data.Add(student);
                  }

              }
              else
                  throw new Exception("error");

              string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

              //write string to file
              System.IO.File.WriteAllText(strFile, json);

              return student;
          }*/

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetAllProducts")]
        public List<Product> GetAllProducts()
        {
            List<Product> _data = getData();

            return _data;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("GetProductById")]
        public Product GetProductById(int productId)
        {
            List<Product> _data = getData();

            var student = _data.FirstOrDefault(r => r.productId == productId);

            return student;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("DeleteProduct")]
        public Product DeleteProduct(int studentId)
        {
            List<Product> _data = getData();

            var student = _data.FirstOrDefault(r => r.productId == studentId);
            var removedProduct = student;
            _data.Remove(student);

            string json = JsonConvert.SerializeObject(_data.ToArray(), Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText(strFile, json);

            return removedProduct;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public string Get()
        {

            return "success";
        }

        private List<Product> getData()
        {

            List<Product> _data = new List<Product>();
            using (StreamReader r = new StreamReader(strFile))
            {
                string strJson = r.ReadToEnd();
                if (strJson != "")
                {
                    _data = JsonConvert.DeserializeObject<List<Product>>(strJson);
                }
            }

            return _data;
        }
    }

    public class Product
    {
        public int productId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string reelaseDate { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public decimal starRating { get; set; }
        public string imageUrl { get; set; }
    }
}