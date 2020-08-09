using UnityEngine;
using System.Collections;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class limitFramerate : MonoBehaviour {
    void Awake() {
        Application.targetFrameRate = 60;
    }
}
}