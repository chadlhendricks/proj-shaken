using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ExamineSystem : MonoBehaviour
{
    [SerializeField] private GameObject _examinePoint;
    // [SerializeField] private GameObject VFX_Effect;
    [SerializeField] private GameObject _infoUI;

    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private CursorIcon _cursorIcon;

    [SerializeField] private Camera _examineCamera;
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private GameObject _inventoryUI;

    public GameObject ExamineObject;
    public GameObject OriginalParentObject;

    public Vector3 parentSize;
    private Vector3 OriginalScale;
    private Vector3 OriginalPosition;
    private Quaternion OriginalRotation;

    public int OriginalLayer;
    public static bool ExamineMode;

    public string content;
    public string header;

    public void Start()
    {
        // VFX_Effect = GameObject.Find("DoF-Effect");
        parentSize = _examinePoint.transform.GetComponent<BoxCollider>().bounds.size / 2;
        _infoUI.GetComponentInChildren<Button>(true).onClick.AddListener(ExitExamineMode);
        _infoUI.SetActive(false);
        // VFX_Effect.SetActive(false);
        _inventoryUI.SetActive(false);
        ExamineMode = false;
    }

    public void ExamineAction(GameObject item) 
    {
        ExamineMode = true;
        _infoUI.SetActive(true);
        // VFX_Effect.SetActive(true);
        ExamineObject = item;
        ItemInspect(ExamineObject);
    }

    public void ExamineActionPickUp()
    {
        ExamineObject.GetComponent<InspectSystem>().has_pickup = true;
        _playerInventory.AddToInventory(ExamineObject);
        ExitExamineMode();
    }

    #region Item Inspection
    public void ItemInspect(GameObject Inspect_object)
    {
        if (TooltipTrigger.EximaneFromInventory) 
        {
            TooltipSystem.Hide();
            ExamineMode = true;
            _infoUI.SetActive(true);
            // VFX_Effect.SetActive(true);
            ExamineObject = Inspect_object;
            ExamineObject.SetActive(true);

            _inventoryUI.SetActive(false);
            PlayerInventory.InventoryIsOn = false;
        }

        if (ExamineObject.transform.parent != null)
            OriginalParentObject = ExamineObject.transform.parent.gameObject;
        else
            OriginalParentObject = null;

        this.gameObject.GetComponent<FirstPersonController>().enabled = false;
        SetNewTransform(Inspect_object);
        ResizeObject(Inspect_object);
    }
    #endregion

    #region Reset Tranform
    public void ResetTransform()
    {
        _examineCamera.fieldOfView = 50f;
        ExamineObject.transform.SetParent(_examinePoint.transform);
        ExamineObject.transform.localPosition = new Vector3(0, 0, 0);
        ExamineObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        ExamineObject.layer = 3;
    }
    #endregion

    #region Set old and new Transform
    public void SetNewTransform(GameObject Inspect_object) 
    {
        OriginalPosition = Inspect_object.transform.position;
        OriginalRotation = Inspect_object.transform.rotation;
        OriginalScale = Inspect_object.transform.localScale;
        OriginalLayer = Inspect_object.layer;
        Inspect_object.transform.SetParent(_examinePoint.transform);
        Inspect_object.transform.localPosition = new Vector3(0, 0, 0);
        Inspect_object.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Inspect_object.layer = 3;
        foreach (Transform child in Inspect_object.transform)
        {
            child.gameObject.layer = 3;
        }
    }
    #endregion

    #region Exit Examine Mode
    public void ExitExamineMode() 
    {
        _cursorIcon.ChangeMouseIcon(CursorLockMode.Locked, false, Color.white, 5);

        this.gameObject.GetComponent<FirstPersonController>().enabled = true;
        ExamineMode = false;
        // VFX_Effect.SetActive(false);
        _infoUI.SetActive(false);


        if (OriginalParentObject != null)
            ExamineObject.transform.parent = OriginalParentObject.transform;
        else
            ExamineObject.transform.parent = null;

        ExamineObject.transform.position = OriginalPosition;
        ExamineObject.transform.rotation = OriginalRotation;
        ExamineObject.transform.localScale = OriginalScale;
        ExamineObject.layer = OriginalLayer;

        foreach (Transform child in ExamineObject.transform)
        {
            child.gameObject.layer = OriginalLayer;
        }

        if (ExamineObject.GetComponent<InspectSystem>().has_pickup)
            ExamineObject.SetActive(false);

        if (TooltipTrigger.EximaneFromInventory)
        {
            _cursorIcon.ChangeMouseIcon(CursorLockMode.None, true, Color.black, 5);

            this.gameObject.GetComponent<FirstPersonController>().enabled = false;
            _inventoryUI.SetActive(true);
            PlayerInventory.InventoryIsOn = true;
            TooltipTrigger.EximaneFromInventory = false;
        }
    }
    #endregion

    #region Resize
    public void ResizeObject(GameObject Resize_object)
    {
        _mainCamera.transform.localPosition = new Vector3(0, 0.8f, 0);
        _mainCamera.localRotation = new Quaternion(0, 0, 0, 0);


        if (Resize_object.GetComponent<MeshFilter>() != null)
        { 
            while (Resize_object.transform.GetComponent<MeshFilter>().GetComponent<Renderer>().bounds.extents.x > parentSize.x ||
                  Resize_object.transform.GetComponent<MeshFilter>().GetComponent<Renderer>().bounds.extents.y > parentSize.y ||
                  Resize_object.transform.GetComponent<MeshFilter>().GetComponent<Renderer>().bounds.extents.z > parentSize.z)
            {
                    Resize_object.transform.localScale *= 0.9f;
            }
        }
    }
    #endregion
}

