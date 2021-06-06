using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.App.Models
{
    public class CreateCategoryResponse : BaseResponse
    {
        public CategoryViewModel Category { get; set; }
    }
}
