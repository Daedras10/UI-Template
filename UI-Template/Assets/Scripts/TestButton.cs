using UnityEngine;
using UnityEngine.UIElements;

public class TestButton : MonoBehaviour
{
    [SerializeField] private UIDocument menuUiDocument;

    private VisualElement root;
    
    // Start is called before the first frame update
    void Start()
    {
        root = menuUiDocument.rootVisualElement;
        
        
        //var button = root.Q<Button>("SUUButon");
        //button.RegisterCallback<ClickEvent, string>(SuuButtonClicked, "SUUUUUUUU !");
        //button.RegisterCallback<ClickEvent>(ev => Debug.Log("SUUUUUUUUUUUUUUU !"));
        
        /*
         * Q<VisualElement>(name:"") : Recherche un élément dans l'arbre de la racine
         * Query<VisualElement>(className:"").ToList() : Recherche tous les éléments ayant la classe
         * Q<VisualElement>(className:"").Q<VisualElement>(name:"")
         *
         * First, Last, AtIndex, Children, Where
         *
         * Buttons : :active (mouse down), :hover (mouse over), :disabled, :disabled with PickingMode.Ignore (no active)
         * :focus (keyboard), :checked
         *
         * USS transitions > Juice, Animation, Smooth transitions like fades
         * Exemple : .myClass { transition-duration: 0.5s; transition-property: opacity; transition-timing-function: ease-in-out; }
         * Exemple : .myClass { transition: background-color 0.5s ease-in-out; }
         *
         * Can have multiple : .classA { transition-property: scale, background-color;
         * transition-duration: 1s, 0.5s;
         * transition-delay: 0s, 600ms;
         * transition-timing-function: ease-in-sine, ease-out-elastic; }
         *
         * translate value : translate(10px, 20px)
         *
         * Events :
         * -ClickEvent : click
         * -ChangeEvent : value changed (TextField, Toogle, Slider, etc.)
         *          .newData, .previousData
         * -PointerEvent
         * -MouseEvent
         * -FocusEvent
         * -GeometryChangedEvent
         *
         * Propagate (from child to parent) : StopPropagation() stop the propagation
         * StopImmediatePropagation() stop the propagation and the event is not handled by other handlers
         * Event can be Tickle.down or Bubble.up (default, ignore the Tickle)
         * >> Allow to setup one Handler for multiple elements, placed on the root
         *
         * Manipulators (like handlers) : Clickable, Focusable, PointerDown, PointerUp, PointerMove, PointerEnter, PointerLeave, PointerCancel, MouseDown, MouseUp, MouseMove, MouseEnter, MouseLeave, MouseWheel, KeyDown, KeyUp, KeyPress, GeometryChanged
         * -Dragger : DragStart, DragPerform, DragExited, DragEnter, DragLeave, DragUpdated
         * -Multi-Touch : TouchStart, TouchEnd, TouchMove, TouchCancel
         *
         * Manipulator (origin inherit), KeyboardNavigationManipulator, MouseManipulator, PointerManipulator,
         * TouchManipulator, DragAndDropManipulator, Clickable (mouse event on element, ContextualMenuManipulator (contextual menu on right click or button press)
         * - extends Manipulator (or subclass)
         * - Override methods : RegisterCallbacksOnTarget, UnregisterCallbacksFromTarget
         * - AddManipulator method to VisualElement
         */
    }
    
    private void SuuButtonClicked(ClickEvent clickEvent, string suu)
    {
        // Debug.Log(suu);
        //
        // var button = root.Q<Button>("SUUButon");
        // Debug.Log($"{button.transform.position}");
        //
        // button.style.position = Position.Absolute;
        // button.style.left =  Length.Percent(Random.Range(5, 95));
        // button.style.top = Length.Percent(Random.Range(5, 95));
        //
        // //button.transform.position += new Vector3(+10, +5, 0);
        //
        // var display = root.Q("b1").style.display;
        //
        // root.Q("b1").style.display = (display == DisplayStyle.None) ? DisplayStyle.Flex : DisplayStyle.None;
        // root.Q("b2").style.display = (display == DisplayStyle.None) ? DisplayStyle.Flex : DisplayStyle.None;
        
    }
}
