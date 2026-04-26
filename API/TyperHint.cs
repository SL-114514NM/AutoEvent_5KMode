using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Arguments;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API
{
    public class TyperHint
    {
        private class InternalHint : AbstractHint
        {
            public string FullText { get; set; } = string.Empty;
            public int CurrentCharIndex { get; set; } = 0;
            public float TypeSpeed { get; set; } = 0.05f;
            public float WaitAfterComplete { get; set; } = 2f;
            public DateTime? TypingCompletedAt { get; set; } = null;
            public Action OnComplete { get; set; } = null;
            public bool WillHide { get; set; } = true;
            public bool HasTriggeredComplete { get; set; } = false;
            public bool IsTypingComplete => CurrentCharIndex >= FullText.Length;
        }
        public string PrefixName { get; set; } = string.Empty;
        public string TextMessage { get; set; } = string.Empty;
        public List<string> TextMessages { get; set; } = new List<string>();
        public string VoiceMessage { get; set; } = string.Empty;
        public List<string> VoiceMessages { get; set; } = new List<string>();
        public float TypeSpeed { get; set; } = 0.05f;
        public float WaitAfterComplete { get; set; } = 2f;
        public AbstractHint HintBase { get; private set; }
        public Hint Hint
        {
            get
            {
                return HintBase as Hint;
            }
        }
        private InternalHint currentHint;
        private PlayerDisplay currentDisplay;
        private int currentTextIndex = 0;
        private int currentVoiceIndex = 0;
        private bool willHide = true;
        public void Send(Player player, bool willHide = true)
        {
            if (player == null) return;

            currentDisplay = PlayerDisplay.Get(player);
            if (currentDisplay == null) return;

            this.willHide = willHide;
            currentTextIndex = 0;
            currentVoiceIndex = 0;

            PlayNext();
        }
        private void PlayNext()
        {
            bool hasMoreText = TextMessages.Count > 0
                ? currentTextIndex < TextMessages.Count
                : (currentTextIndex == 0 && !string.IsNullOrEmpty(TextMessage));

            bool hasMoreVoice = VoiceMessages.Count > 0
                ? currentVoiceIndex < VoiceMessages.Count
                : (currentVoiceIndex == 0 && !string.IsNullOrEmpty(VoiceMessage));

            if (hasMoreText)
            {
                string text = TextMessages.Count > 0
                    ? TextMessages[currentTextIndex]
                    : TextMessage;

                currentTextIndex++;
                ShowTypewriter(text, () => PlayNext());
            }
            else if (hasMoreVoice)
            {
                string voice = VoiceMessages.Count > 0
                    ? VoiceMessages[currentVoiceIndex]
                    : VoiceMessage;

                currentVoiceIndex++;
                PlayVoice(voice);
                PlayNext();
            }
        }
        private void ShowTypewriter(string message, Action onComplete)
        {
            if (currentDisplay == null) return;

            string fullText = string.IsNullOrEmpty(PrefixName)
                ? message
                : $"{PrefixName}: {message}";

            currentHint = new InternalHint
            {
                Id = $"cassie_{Guid.NewGuid():N}",
                FullText = fullText,
                TypeSpeed = TypeSpeed,
                WaitAfterComplete = WaitAfterComplete,
                FontSize = 18,
                SyncSpeed = HintSyncSpeed.Fast,
                WillHide = willHide,
                OnComplete = onComplete
            };

            currentHint.AutoText = (AutoContentUpdateArg arg) =>
            {
                if (currentHint.IsTypingComplete)
                {
                    currentHint.TypingCompletedAt = DateTime.Now;

                    if ((DateTime.Now - currentHint.TypingCompletedAt.Value).TotalSeconds >= currentHint.WaitAfterComplete)
                    {
                        if (currentHint.WillHide)
                        {
                            currentDisplay.RemoveHint(currentHint);
                        }
                        else if (!currentHint.HasTriggeredComplete)
                        {
                            currentHint.HasTriggeredComplete = true;
                            currentHint.OnComplete?.Invoke();
                        }
                        return currentHint.FullText;
                    }

                    return currentHint.FullText;
                }

                currentHint.CurrentCharIndex = Math.Min(
                    currentHint.CurrentCharIndex + 1,
                    currentHint.FullText.Length
                );

                string displayText = currentHint.FullText.Substring(0, currentHint.CurrentCharIndex);
                if (!currentHint.IsTypingComplete)
                {
                    displayText += "<alpha=#AA>|</alpha>";
                }

                return displayText;
            };

            HintBase = currentHint;
            currentDisplay.AddHint(currentHint);
        }
        private void PlayVoice(string voice)
        {
            if (currentDisplay?.ReferenceHub == null) return;
            SAPI.CassieMessage(voice);
        }
        public void Stop()
        {
            if (currentHint != null && currentDisplay != null)
            {
                currentDisplay.RemoveHint(currentHint);
            }
        }
        public void Skip()
        {
            if (currentHint != null)
            {
                currentHint.CurrentCharIndex = currentHint.FullText.Length;
            }
        }
    }
}
