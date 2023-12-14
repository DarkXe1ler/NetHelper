using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class QuizBehaviour : MonoBehaviour
{
    [SerializeField] private Button ans1, ans2, ans3, ans4, next;
    [SerializeField] private TMP_Text text;
    //[SerializeField] private Image? img;
    [SerializeField] List<Question> questions;
    private int cur, correct;
    List<int> chosenQuestions;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new();
        chosenQuestions = new()
        {
            rnd.Next(0, 4),
            rnd.Next(4, 7),
            rnd.Next(7, 11),
            11
        };
        RandomShuffle<int>(chosenQuestions);
        cur = 0; correct = 0;
        SetupQuestion(questions[chosenQuestions[cur]]);
        next.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // можно удалить?
    }

    void RandomShuffle<T>(List<T> list)
    {
        System.Random rnd = new();
        int n = list.Count;
        while (n > 1)
        {
            int k = rnd.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void SetupQuestion(Question q)
    {   
        text.text = q.question;
        if (q.img != null)
        {
            // здесь нужно вставить изображение на экран
        }
        List<string> setup = new(q.answers);
        RandomShuffle<string>(setup);
        ans1.GetComponentInChildren<TMP_Text>().text = setup[0];
        ans2.GetComponentInChildren<TMP_Text>().text = setup[1];
        if (setup.Count > 2)
        {
            ans3.gameObject.SetActive(true);
            ans3.GetComponentInChildren<TMP_Text>().text = setup[2];
        }
        else ans3.gameObject.SetActive(false);
        if (setup.Count > 3)
        {
            ans4.gameObject.SetActive(true);
            ans4.GetComponentInChildren<TMP_Text>().text = setup[3];
        }
        else ans4.gameObject.SetActive(false);
    }

    public void AnswerClick(Button chosen)
    {
        if (!next.gameObject.activeSelf) {
            if (chosen.GetComponentInChildren<TMP_Text>().text == questions[chosenQuestions[cur]].answers[0])
            {
                chosen.image.color = Color.green;
                correct++;
            }
            else
            {
                chosen.image.color = Color.red;
            }
            next.gameObject.SetActive(true);
        }
    }

    public void NextClick()
    {
        ans1.image.color = Color.white;
        ans2.image.color = Color.white;
        ans3.image.color = Color.white;
        ans4.image.color = Color.white;
        next.gameObject.SetActive(false);
        cur++;
        if (cur < chosenQuestions.Count)
        {
            SetupQuestion(questions[chosenQuestions[cur]]);
        } else
        {
            ans1.gameObject.SetActive(false);
            ans2.gameObject.SetActive(false);
            ans3.gameObject.SetActive(false);
            ans4.gameObject.SetActive(false);
            if (correct == 1) text.text = $"Тест завершён. Вы ответили правильно на {correct} вопрос из {chosenQuestions.Count}.";
            else if ((correct > 1) && (correct < 5)) text.text = $"Тест завершён. Вы ответили правильно на {correct} вопроса из {chosenQuestions.Count}.";
            else text.text = $"Тест завершён. Вы ответили правильно на {correct} вопросов из {chosenQuestions.Count}.";
        }
    }

    [System.Serializable]
    public class Question
    {
        [SerializeField] public string question;
        [SerializeField] public List<string> answers; // Set the correct answer as answers[0]
        //[SerializeField] public string correct; // deprecated
        [SerializeField] public Image img;

        Question(string q, List<string> a)
        {
            question = q;
            answers = a;
        }
    }
}
