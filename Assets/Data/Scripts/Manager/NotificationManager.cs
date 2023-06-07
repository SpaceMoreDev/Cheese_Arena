using UnityEngine;
using System;
using TMPro;

namespace Managers
{
    public class NotificationManager : MonoBehaviour
    {
        [SerializeField] TMP_Text notificationLabel;
        [SerializeField] GameObject notifications;
        [SerializeField] float notificationHideDelay = 2f;

        public static event Action startNotification;
        public static NotificationManager current;
        
        void Awake()
        {
            current = this;
        }

        public static void StartNotification (string text)
        {
            TMP_Text label = Instantiate(current.notificationLabel, current.notifications.transform);
            label.transform.SetAsFirstSibling();
            label.GetComponent<NotificationLabel>().FadeTime = current.notificationHideDelay;
            label.text = text;
            Destroy(label.gameObject,current.notificationHideDelay);

            // do stuff when player enters camera trigger
           if(startNotification != null)
           {
                startNotification();
           }
        }
    }
}
