﻿using System;
using System.Globalization;
using System.Windows.Data;
using Drachenhorn.Core.Lang;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Desktop.UI.Lang
{
    public class TranslateExtension : Binding
    {
        /// <summary>
        ///     Translates the given TranslateID
        /// </summary>
        /// <param name="name">TranslateID</param>
        public TranslateExtension(string name) : base("[%" + name + "]")
        {
            try
            {
                if (ViewModelBase.IsInDesignModeStatic)
                    Source = new LanguageManager() {CurrentCulture = CultureInfo.CurrentCulture};
                else
                    Source = SimpleIoc.Default.GetInstance<LanguageManager>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}