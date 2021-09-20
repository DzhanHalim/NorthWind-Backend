using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
   public static class Messages
    {
        public static string ProductAdded = "Product has been added";
        public static string ProductNameInvalid = "Invalid Product Name";
        public static string ProductListed = "Product has been listed";
        public static string MaintenanceTime = "System in maintenance";
        public static string ProductDeleted = "Product has been deleted";
        public static string ProductUpdated = "Product has been updated";

        public static string ProductCountOfCategoryError = "Products for this category reached max";
        public static string NameAlreadyExcist = "This name already exist";
        public static string CategoryLimitExceded = "Category limit exceded";
        public static string AuthorizationDenied ="You dont have permission";
        public static string UserNotFound="User not found";
        public static string PasswordError="Password is incorect";
        public static string SuccessfulLogin="Successful login";
        public static string UserAlreadyExists="User already exists";
        public static string AccessTokenCreated="Access token created";

        public static string UserRegistered = "Usser registered succesful";
    }
}
