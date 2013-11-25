using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS2.WEB.Models.ViewModels
{
    public class ContentCreateViewModel
    {
        public Content Content { get; set; }
        public List<Page> Pages { get; set; }

        public List<SelectListItem> SelectItems
        {
            get
            {
                return Pages.Select(p => new SelectListItem
                {
                    Text = p.Title,
                    Value = p.Id.ToString()
                }).ToList();
            }
        }
    }
}