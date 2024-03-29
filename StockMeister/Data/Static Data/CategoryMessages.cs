﻿namespace StockMeister.Data.Static_Data
{
    public static class CategoryMessages
    {
        public static int success_code = 1;
        public static int failure_code = 0;

        public static string categoryCreated = "Category Created";
        public static string categoryUpdated = "Category Updated";
        public static string categoryDeleted = "Category Deleted";
        public static string categoryUsed = "Cannot delete a category being used for a product";

        public static string somethingWentWrong = "Something went wrong, please try again";
    }
}