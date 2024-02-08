using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Stores the Previous Input Value.
    private float prevInput;
    // Lets us know if we should clear the Previous Input.
    private bool clearPrevInput;
    // Declares the equationType variable which is an enum.
    private EquationType equationType;
    // Clears Calculator when the script starts.
    private void Start()
    {
        Clear();
    }

    public void AddInput(string input)
    {
        if (clearPrevInput)
        {
            text.text = "";
            clearPrevInput = false;
        }

        text.text += input;
    }
    // A Personal Function to test how C# converts numbers and dots to floats/strings.
    public void Test()
    {
        prevInput = float.Parse(text.text);
        text.text = "testing...";
        text.text = prevInput.ToString();
    }
    // All the below sets the Equation Type.
    public void SetEquationAsAdd()
    {
        prevInput = float.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.Add;
    }
    
    public void SetEquationAsSubtract()
    {
        prevInput = float.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.Subtract;
    }
    
    public void SetEquationAsMultiply()
    {
        prevInput = float.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.Multiply;
    }
    
    public void SetEquationAsDivide()
    {
        prevInput = float.Parse(text.text);
        clearPrevInput = true;
        equationType = EquationType.Divide;
    }
    // All the below performs the math.
    private void Add()
    {
        float currentInput = float.Parse(text.text);
        float result = prevInput + currentInput;

        text.text = result.ToString();
    }
    
    private void Subtract()
    {
        float currentInput = float.Parse(text.text);
        float result = prevInput - currentInput;

        text.text = result.ToString();
    }

    private void Multiply()
    {
        float currentInput = float.Parse(text.text);
        float result = prevInput * currentInput;

        text.text = result.ToString();
    }
    
    private void Divide()
    {
        float currentInput = float.Parse(text.text);
        // Error Checking
        if (currentInput != 0)
        {
            text.text = "Unable";
        }
        
        float result = prevInput / currentInput;

        text.text = result.ToString();
    }
    // The below uses a switch to pick which math function it should do (I used switch instead of if statements because it feels cleaner).
    public void Calculate()
    {
        switch (equationType)
        {
            case EquationType.Add:
                Add();
                break;
            case EquationType.Subtract:
                Subtract();
                break;
            case EquationType.Multiply:
                Multiply();
                break;
            case EquationType.Divide:
                Divide();
                break;
            default:
                Clear();
                break;
        }
    }
    // The below resets the Calculator.
    public void Clear()
    {
        text.text = "";
        prevInput = 0;
        clearPrevInput = false;
        equationType = EquationType.None;
    }
    // The below creates the Equation Type Enumerator/
    private enum EquationType
    {
        None = 0,
        Add = 1,
        Subtract = 2,
        Multiply = 3,
        Divide = 4
    }
}
