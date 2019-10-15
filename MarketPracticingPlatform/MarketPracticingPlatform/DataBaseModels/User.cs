﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPracticingPlatform.Models;

namespace MarketPracticingPlatform.DataBaseModels
{
    public class User
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Number { get; set; }

        public int BonusScore { get; set; }

    }
}