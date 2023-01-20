using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Threading;

public class VisualRepresentation : MonoBehaviour
{
    private Button _button;
    private List<VisualElement> _elements;
    [SerializeField] private int _signal = 1;

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();
        Debug.Log("Loaded UI");
        
        var _rootVisualElement = uiDocument.rootVisualElement;
        _button = _rootVisualElement.Q<Button>();
        _elements = new List<VisualElement>();
        
        // Get the eight different sensor corresponding visual elements from the ui
        var upperRight = _rootVisualElement.Q<VisualElement>(name: "upperRight");
        _elements.Add(upperRight);
        var upperLeft = _rootVisualElement.Q<VisualElement>(name: "upperLeft");
        _elements.Add(upperLeft);
        var lowerRight = _rootVisualElement.Q<VisualElement>(name: "lowerRight");
        _elements.Add(lowerRight);
        var lowerLeft = _rootVisualElement.Q<VisualElement>(name: "lowerLeft");
        _elements.Add(lowerLeft);
        var rightUp = _rootVisualElement.Q<VisualElement>(name: "rightUp");
        _elements.Add(rightUp);
        var rightDown = _rootVisualElement.Q<VisualElement>(name: "rightDown");
        _elements.Add(rightDown);
        var leftUp = _rootVisualElement.Q<VisualElement>(name: "leftUp");
        _elements.Add(leftUp);
        var leftDown = _rootVisualElement.Q<VisualElement>(name: "leftDown");
        _elements.Add(leftDown);
        
        Debug.Log($"List of visual elements: {_elements}");

        _button.RegisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(PrintClickMessage);
    }
    
    // connect button with visibility of one visual element
    private void PrintClickMessage(ClickEvent evt)
    {

        var button = evt.currentTarget as Button;

        Debug.Log($"{button.name} was clicked!");
        // get old value of color and change visibility
        if (0 <= _signal && _signal < 8)
        {
            VisualElement element = _elements[_signal];
            bool isVisible = element.visible;
            element.visible = !isVisible;
        }
        else
        {
            Debug.Log($"{_signal} is out of bounds (range 0-7)!");
        }
        
    }

}

