﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Drachenhorn.Core.Lang;
using GalaSoft.MvvmLight.Ioc;

namespace Drachenhorn.Core.UI
{
    [Flags]
    public enum MessageIcon
    {
        None = 0,
        Info = 1,
        Warning = 2,
        Error = 4
    }

    public class MessageFactory
    {
        private Action<int> _afterHideCallback;
        private readonly List<string> _buttons = new List<string>();
        private Exception _exception;
        private MessageIcon _icons;
        private string _message;
        private string _title = "Message";

        public static MessageFactory NewMessage()
        {
            return new MessageFactory();
        }


        public MessageFactory Exception(Exception e)
        {
            _exception = e;
            return this;
        }

        public MessageFactory Message(string message)
        {
            _message = message;
            return this;
        }

        public MessageFactory MessageTranslated(string message, bool isSingleKey = true)
        {
            if (isSingleKey)
                _message = LanguageManager.Translate(message);
            else
                _message = LanguageManager.TextTranslate(message);

            return this;
        }

        public MessageFactory Title(string title)
        {
            _title = title;
            return this;
        }

        public MessageFactory TitleTranslated(string title, bool isSingleKey = true)
        {
            if (isSingleKey)
                _title = LanguageManager.Translate(title);
            else
                _title = LanguageManager.TextTranslate(title);

            return this;
        }

        public MessageFactory Button(string buttonTitle, int index = -1)
        {
            if (index > _buttons.Count)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Set index out of Range (start with 0)");

            if (index >= 0)
                _buttons.Insert(index, buttonTitle);
            else
                _buttons.Add(buttonTitle);
            return this;
        }

        public MessageFactory ButtonTranslated(string buttonTitle, int index = -1, bool isSingleKey = true)
        {
            var text = "";

            if (isSingleKey)
                text = LanguageManager.Translate(buttonTitle);
            else
                text = LanguageManager.TextTranslate(buttonTitle);

            Button(text, index);

            return this;
        }

        public MessageFactory Icon(MessageIcon icons)
        {
            if (icons == MessageIcon.None)
                _icons = MessageIcon.None;
            else
                _icons |= icons;

            return this;
        }

        public MessageFactory Callback(Action<int> afterHideCallback)
        {
            _afterHideCallback = afterHideCallback;
            return this;
        }

        public Task<int> ShowMessage(bool allowInternal = true)
        {
            var service = SimpleIoc.Default.GetInstance<IDialogService>();

            if (_exception != null)
                return Task.Run(() =>
                {
                    service.ShowException(_exception, _title, () => { _afterHideCallback?.Invoke(0); }).Wait();
                    return 0;
                });

            if (allowInternal)
                return service.ShowMessage(_message, _title, _buttons, _afterHideCallback);
            return service.ShowMessageExternal(_message, _title, _buttons, _afterHideCallback);
        }
    }
}