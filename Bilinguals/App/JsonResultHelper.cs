using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bilinguals.App
{
    public class JsonResultHelper  //de khi pass sang view ta co the lay gia tri
    {
        public static object MapGroupJson(Group group)
        {
            return new
            {
                id = group.Id,
                name = group.Name,
                userId = group.UserId,
                description = group.Description,
            };
        }

        public static object MapImageJson(Image image)
        {
            return new
            {
                id = image.Id,
                path = image.ImagePath,
            };
        }
    }
}