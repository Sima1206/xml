﻿using Microsoft.EntityFrameworkCore;
using XML.Core;
using XML.Model;

namespace XML.Repository
{
    public class CityRepository : BaseRepository<City>
    {
        public CityRepository(DbContext context) : base(context)
        {
        }
    }
}
