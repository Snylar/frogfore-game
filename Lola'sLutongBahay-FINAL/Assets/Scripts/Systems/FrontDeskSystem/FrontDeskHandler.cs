using UnityEngine;
using TMPro;

namespace LLB
{
    public class FrontDeskHandler : MonoBehaviour
    {

        [SerializeField]
        private CustomerHandler customerHandler;

        [SerializeField]
        private GameObject customerHolder;


        [SerializeField]
        private FoodLibraryHandler foodLibraryHandler;

        [SerializeField]
        private DialougeHandler dialougeHandler;


        [SerializeField]
        private Canvas dialogueCanvas;

        [SerializeField]
        private TMP_Text dialougeText;


        void Start()
        {
            customerHolder.GetComponent<SpriteRenderer>().sprite = null;
            dialogueCanvas.gameObject.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EnterCustomer();
                Debug.Log("Space key was pressed!");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                RemoveCustomerToDesk();
                Debug.Log("Space A was pressed!");
            }
        }

        void EnterCustomer()
        {
            customerHandler.GetCustomer();

            if (customerHandler.currentCustomer)
            {
                LoadCustomerToDesk();
                foodLibraryHandler.GenerateRandomOrder();
                dialogueCanvas.gameObject.SetActive(true);

                if (foodLibraryHandler.currentOrder != null)
                { 
                    dialougeText.SetText(dialougeHandler.GenerateOrderDialouge(foodLibraryHandler.currentOrder));
                }
                
            }
            else
            {
                Debug.LogWarning("There is bug: Should not have currentCustomer");
            }
        }

        void LoadCustomerToDesk()
        {
            customerHolder.GetComponent<SpriteRenderer>().sprite = customerHandler.currentCustomer.sprite;
        }

        void RemoveCustomerToDesk()
        {
            customerHolder.GetComponent<SpriteRenderer>().sprite = null;
            foodLibraryHandler.currentOrder = null;
            dialogueCanvas.gameObject.SetActive(false);
            customerHandler.RemoveCustomer();
            dialougeText.SetText("");
        }

        public void ConfirmCustomerRequest()
        {
            Debug.Log("Processing Request");
        }
    }
}


