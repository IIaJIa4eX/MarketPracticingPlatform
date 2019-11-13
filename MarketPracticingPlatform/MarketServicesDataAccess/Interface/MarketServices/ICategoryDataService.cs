﻿using MarketPracticingPlatform.Service.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPracticingPlatform.Service.Interface
{
    public interface ICategoryDataService
    {
        CategorySearchDTO SearchByCategoryName(string categoryName);
        CategoryCreationDTO CreateCategory(CategoryDTO categoryDTO);
    }
}
