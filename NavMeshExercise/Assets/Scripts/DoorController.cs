using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    enum DoorState {
        Open,
        Closed,
        Opening,
        Closing
    }

    DoorState doorState = DoorState.Closed;

    float transitionTime = 1;
    float transitionDistance = 2;

    Coroutine openingCoroutine = null;
    Coroutine closingCoroutine = null;

    Vector3 startPosition = Vector3.zero;
    Vector3 endPosition = Vector3.zero;

    void Start() {
        startPosition = transform.position;
        endPosition = transform.position + Vector3.down * transitionDistance;
        StartCoroutine(OpenSequence());
        StartCoroutine(CloseSequence());
    }

    public void Open() {
        if ((doorState == DoorState.Closed || doorState == DoorState.Closing)
           && (openingCoroutine == null)) {
            openingCoroutine = StartCoroutine(OpenSequence());
        }
    }

    public void Close() {
        if ((doorState == DoorState.Open || doorState == DoorState.Opening)
           && (openingCoroutine == null)) {
            closingCoroutine = StartCoroutine(OpenSequence());
        }
    }

    IEnumerator OpenSequence() {
        while (doorState == DoorState.Closing) {
            yield return null;
        }
        doorState = DoorState.Opening;
        float startTime = Time.time;
        float endTime = Time.time + transitionTime;
        float dt = 0;
        while (dt + startTime < endTime) {
            float ndt = dt / transitionTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, ndt);
            dt += Time.deltaTime;
            yield return null;
        }
        transform.position = endPosition;
        doorState = DoorState.Open;
    }

    IEnumerator CloseSequence() {
        while (doorState == DoorState.Opening) {
            yield return null;
        }
        doorState = DoorState.Closing;
        float startTime = Time.time;
        float endTime = Time.time + transitionTime;
        float dt = 0;
        while (dt + startTime < endTime) {
            float ndt = dt / transitionTime;
            transform.position = Vector3.Lerp(endPosition, startPosition, ndt);
            dt += Time.deltaTime;
            yield return null;
        }
        transform.position = endPosition;
        doorState = DoorState.Closed;
    }
}
