﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Petrovich.Web.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Petrovich.Web.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Часть инвентарного номера не может быть изменена..
        /// </summary>
        internal static string Branch_InventoryPart_Changed_Error {
            get {
                return ResourceManager.GetString("Branch_InventoryPart_Changed_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Раздел с данной частью инвентарного номера уже зарегистрирован..
        /// </summary>
        internal static string Branch_InventoryPart_Duplicate_Error {
            get {
                return ResourceManager.GetString("Branch_InventoryPart_Duplicate_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Часть инвентарного номера должна быть 2 символа..
        /// </summary>
        internal static string Branch_InventoryPart_StringLength_Error {
            get {
                return ResourceManager.GetString("Branch_InventoryPart_StringLength_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Выбранный раздел не найден..
        /// </summary>
        internal static string Category_BranchNotFound_Error {
            get {
                return ResourceManager.GetString("Category_BranchNotFound_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Часть инвентарного номера не может быть изменена..
        /// </summary>
        internal static string Category_InventoryPart_Changed_Error {
            get {
                return ResourceManager.GetString("Category_InventoryPart_Changed_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Для выбранного раздела все слоты для категорий заняты. Выберите другой раздел либо удалите ненужную категорию..
        /// </summary>
        internal static string Category_NoInventoryPartSlotsAvailable_Error {
            get {
                return ResourceManager.GetString("Category_NoInventoryPartSlotsAvailable_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Критическая ошибка.
        /// </summary>
        internal static string Critical {
            get {
                return ResourceManager.GetString("Critical", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ошибка.
        /// </summary>
        internal static string Error {
            get {
                return ResourceManager.GetString("Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Выбранная категория не найдена..
        /// </summary>
        internal static string Group_CategoryNotFound_Error {
            get {
                return ResourceManager.GetString("Group_CategoryNotFound_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Информационный.
        /// </summary>
        internal static string Information {
            get {
                return ResourceManager.GetString("Information", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Отсутсвует.
        /// </summary>
        internal static string None {
            get {
                return ResourceManager.GetString("None", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Выбранный раздел не найден..
        /// </summary>
        internal static string Product_BranchNotFound_Error {
            get {
                return ResourceManager.GetString("Product_BranchNotFound_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Выбранная категория не найдена..
        /// </summary>
        internal static string Product_CategoryNotFound_Error {
            get {
                return ResourceManager.GetString("Product_CategoryNotFound_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Выбранная группа не найдена..
        /// </summary>
        internal static string Product_GroupNotFound_Error {
            get {
                return ResourceManager.GetString("Product_GroupNotFound_Error", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле {0} обязательно для заполнения..
        /// </summary>
        internal static string Required_Field_Error {
            get {
                return ResourceManager.GetString("Required_Field_Error", resourceCulture);
            }
        }
    }
}
