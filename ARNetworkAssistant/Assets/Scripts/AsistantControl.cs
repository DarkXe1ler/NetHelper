using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class AsistantControl : MonoBehaviour
{
    [SerializeField]
    private GameObject ARCamera;
    [SerializeField]
    private GameObject AssistantPrefab;

    private GameObject Assistant;
    private GameObject EndPoint;

    [SerializeField]
    private Transform AssistantPosition;

    [SerializeField]
    private GameObject Scroll;

    [SerializeField]
    private VideoClip videCl;
    [SerializeField]
    private int SecondsVideo = 15;

    private delegate void MoveRobot();
    private MoveRobot moveRobot;


    void Start()
    {
        Assistant = Instantiate(AssistantPrefab, ARCamera.transform.position + new Vector3(4f, 0, 0), ARCamera.transform.rotation);
        EndPoint = new GameObject("EmptyObject");
        EndPoint.transform.position = Assistant.transform.position;
        VideoPlayer player = Assistant.GetComponent<VideoPlayer>();
        player.clip = videCl;
        Scroll.SetActive(false);
        moveRobot += MoveRobotCamera;
        StartCoroutine(routine: CoroutineSample());
    }

    void Update()
    {
        moveRobot?.Invoke();
    }

    private void MoveRobotCamera()
    {
        if (CheckDist(Assistant.transform.position, AssistantPosition.transform.position) >= 0.1f)
        {
            MoveObjToPos(AssistantPosition.position);
        }
        Assistant.transform.LookAt(ARCamera.transform);
    }

    private void MoveEnd()
    {
        if(CheckDist(Assistant.transform.position, EndPoint.transform.position) > 0.1f)
        {
            MoveObjToPos(EndPoint.transform.position);
            Assistant.transform.LookAt(ARCamera.transform);
        }
        else 
            Assistant.SetActive(false);


    }

    public float CheckDist(Vector3 posObjFirst, Vector3 posObjSecond)
    {
        float dist = Vector3.Distance(posObjFirst, posObjSecond);
        return dist;
    }

    private void MoveObjToPos(Vector3 pos)
    {
        Assistant.transform.position = Vector3.Lerp(Assistant.transform.position, pos, 1f * Time.deltaTime);
    }

    private IEnumerator CoroutineSample()
    {
        yield return new WaitForSeconds(SecondsVideo);
        moveRobot -= MoveRobotCamera;
        moveRobot += MoveEnd;
        Scroll.SetActive(true);
    }
}
