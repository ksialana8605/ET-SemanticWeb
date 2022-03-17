using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;

namespace A2SemanticWeb.Pages
{
    public class Product
    {
        public string Name;
        public string ThumbnailUrl;
        public int Quantity;
        public Decimal Price;
    }

    public class IndexModel : PageModel
    {
        private const string PRODUCT_TAG = "product";
        private const string NAME_TAG = "name";
        private const string THUMBNAIL_TAG = "thumbnail";
        private const string QUANTITY_TAG = "quantity";
        private const string PRICE_TAG = "price";

        [BindProperty]
        public string XmlInput { get; set; }

        [BindProperty]
        public List<Product> Products { get; } = new List<Product>();
        public int iterator;
        public string name;
        public string thumbnail;
        public string quantity;
        public string price;

        public IActionResult OnPost()
        {
            this.Products.Clear();
            
            try
            {
                /*
                 * TODO #1: The code below reads XML input from the textarea and sequentially outputs the data it encounters, 
                 *          Modify the code so that the Products variable of List<Product> type
                 *          declared in this class is populated with same data from the XML input.
                 * 
                 *          Once completed, proceed to TODO #2 and 3 inside of the .cshtml to display the product list in a presentable, styled manner
                 */

                XmlTextReader reader = new XmlTextReader(new System.IO.StringReader(this.XmlInput));
                
                while (reader.Read())
                {
                    
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            System.Diagnostics.Debug.WriteLine($"Open tag encountered, tag name: {reader.Name}");
                            break;

                        case XmlNodeType.Text:
                            System.Diagnostics.Debug.WriteLine($"Node value for tag name: {reader.Value}");
                            // Adding the products 
                            iterator++;
                            if (iterator == 1)
                                name = reader.Value; // adding product name
                            if (iterator == 2)
                                thumbnail = reader.Value; //adding product thumbnail
                            if (iterator == 3)
                                quantity = reader.Value; // adding product quanitity
                            if (iterator == 4)
                                price = reader.Value; // adding product price
                            break;

                        case XmlNodeType.EndElement:
                            System.Diagnostics.Debug.WriteLine($"Close tag encountered, tag name: {reader.Name}");
                            break;
                    }
                    
                    
                    // adding product to the list
                    if (iterator % 4 == 0 && iterator != 0)
                    {
                        Products.Add(new Product {
                            Name = name, 
                            Price = Convert.ToDecimal(price),
                            Quantity = Convert.ToInt32(quantity),
                            ThumbnailUrl = thumbnail,
                        }); // products are added
                        iterator = 0;
                    }
                }
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Bad XML Input: {ex.Message}");
            }
            
            
            return Page();
        }
    }
}
