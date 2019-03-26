namespace VRTK.Examples
{
    using UnityEngine;

    public class OptionsMenu : MonoBehaviour
    {
        public VRTK_ControllerEvents leftController;
        public VRTK_ControllerEvents rightController;
        public GameObject controlObject;

        protected bool state;
        Transform InitialParent;
        bool once;
        int counter=0;
        public Vector3 handDistance;

        void Start()
        {
            InitialParent = transform.parent;
            once = false;
            handDistance = new Vector3(0, 0.1f, 0);
        }
        private void Update()
        {

            if (state)
            {
                if (!once)
                {

                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.position = (leftController.transform.position);
                    //controlObject.transform.position = handDistance;

                    /* transform.position = leftController.transform.position;
                    if (counter % 2 != 0)
                    {
                        transform.Rotate(0, -180f, transform.rotation.z, Space.World);
                    }
                    else
                    {
                        transform.Rotate(0, 180, transform.rotation.z, Space.World);
                    }
                    once = true;*/
                }

                transform.SetParent(leftController.transform);
                //transform.position = leftController.transform.position;
                //transform.localRotation = Quaternion.LookRotation(-leftController.transform.forward);
            }
        }

        protected virtual void OnEnable()
        {
            state = false;
            Debug.Log("PrintOnEnable: script was enabled");
            RegisterEvents(leftController);
            RegisterEvents(rightController);
            SetObjectVisibility();
        }

        protected virtual void RegisterEvents(VRTK_ControllerEvents events)
        {
            if (events != null)
            {
                events.ButtonTwoPressed += ButtonTwoPressed;
            }
        }

        protected virtual void ButtonTwoPressed(object sender, ControllerInteractionEventArgs e)
        {
            state = !state;
            once = false;
            transform.parent = InitialParent;
            //Move();
            SetObjectVisibility();
        }

        /*protected virtual void Move()
        {
            Transform playArea = VRTK_DeviceFinder.PlayAreaTransform();
            Transform headset = VRTK_DeviceFinder.HeadsetTransform();
            if (playArea != null && headset != null)
            {
                transform.position = new Vector3(headset.position.x, playArea.position.y, headset.position.z);
                //controlObject.transform.localPosition = headset.forward * 0.5f;
                //controlObject.transform.localPosition = new Vector3(controlObject.transform.localPosition.x, 0f, controlObject.transform.localPosition.z);
                Vector3 targetPosition = headset.position;
                targetPosition.y = playArea.transform.position.y;
                //controlObject.transform.LookAt(targetPosition);
                //controlObject.transform.localEulerAngles = new Vector3(0f, controlObject.transform.localEulerAngles.y, 0f);
            }
        }*/

        protected virtual void SetObjectVisibility()
        {
            controlObject.SetActive(state);
        }
    }
}