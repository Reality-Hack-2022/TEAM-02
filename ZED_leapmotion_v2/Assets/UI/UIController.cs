using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject CompleteUI;
    [SerializeField] private GameObject RingUI;
    [SerializeField] private GameObject CheckUI;

    [SerializeField] private GameObject MoveUI;
    [SerializeField] private GameObject PressUI;
    [SerializeField] private GameObject DirUI;
    [SerializeField] private GameObject ArrowUI;

    [SerializeField] private GameObject TextPanelUI;

    [SerializeField] private float duration = 1.0f;

    private Sequence _canvasSequence;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            PressComplete(RingUI.GetComponent<Image>(), CheckUI.GetComponent<Image>());
        }
        if (Input.GetKey("s"))
        {
            MassageHint(PressUI.GetComponent<Image>(), DirUI.GetComponent<Image>(), ArrowUI.GetComponent<Image>());
        }
        if (Input.GetKey("d"))
        {
            TextAnim(TextPanelUI);
        }

    }

    public void PublicMassageHint()
    {
        MassageHint(PressUI.GetComponent<Image>(), DirUI.GetComponent<Image>(), ArrowUI.GetComponent<Image>());
    }

    // massage direction motion graphics
    public void MassageHint(Image press, Image dir, Image arrow)
    {
        float barVal = dir.fillAmount = 0f;
        Vector3 pressX = press.GetComponent<Transform>().localPosition;
        Vector3 arrowX = arrow.GetComponent<Transform>().localPosition;
        _canvasSequence?.Kill();
        _canvasSequence = DOTween.Sequence();
        press.GetComponent<Transform>().localScale = new Vector3(0.1f, .1f, 0.1f);
        MoveUI.SetActive(true);
        ArrowUI.SetActive(true);
        _canvasSequence.Append(press.GetComponent<Transform>().DOScale(new Vector3(1f, 1f, 1f), duration));
        _canvasSequence.Append(DOTween.To(() => barVal, incrementer => barVal = incrementer, 1, duration)
            .OnUpdate(() =>
            {
                float newVal = dir.fillAmount;
                newVal = barVal;
                dir.fillAmount = newVal;
            })
            .OnComplete(() =>
            {
                ArrowUI.SetActive(true);
            })
        );
        _canvasSequence.Join(arrow.GetComponent<Transform>().DOLocalMoveX(35, duration));
        _canvasSequence.Append(press.GetComponent<Transform>().DOLocalMoveX(35, duration))
            .OnComplete(() =>
            {
                MoveUI.gameObject.SetActive(false);
                press.GetComponent<Transform>().localPosition = pressX;
                arrow.GetComponent<Transform>().localPosition = arrowX;
                  
            });
    }

    public void PublicPressComplete()
    {
        PressComplete(RingUI.GetComponent<Image>(), CheckUI.GetComponent<Image>());
    }

    //complete motion graphics
    private void PressComplete(Image ring, Image check)
    {
        float barVal = ring.fillAmount = 0f;
        float alpha = 0f;

        CompleteUI.gameObject.SetActive(true);
        CheckUI.gameObject.SetActive(false);
        _canvasSequence?.Kill();
        _canvasSequence = DOTween.Sequence();
        _canvasSequence.Append(DOTween.To(() => barVal, incrementer => barVal = incrementer, 1, duration)
            .OnUpdate(() =>
            {
                float newVal = ring.fillAmount;
                newVal = barVal;
                ring.fillAmount = newVal;
            }
            )
        );
        _canvasSequence.Append(DOTween.To(() => alpha, incrementer => alpha = incrementer, 1, duration)
            .OnUpdate(() =>
            {
                CheckUI.gameObject.SetActive(true);
                Color newColor = check.color;
                newColor = new Color(1f, 1f, 1f, alpha);
                check.color = newColor;
            }
            )
            .OnComplete(() =>
            {
                CompleteUI.gameObject.SetActive(false);
            })
        );
    }

    //fade text motion
    public void TextAnim(GameObject textPanel)
    {
        TMP_Text[] tmps = textPanel.GetComponentsInChildren<TMP_Text>();
        Debug.Log(tmps);
        float alpha = 0f;
        DOTween.To(() => alpha, incrementer => alpha = incrementer, 1, duration)
            .OnUpdate(() =>
            {
                textPanel.SetActive(true);
                foreach (TextMeshPro tmp in tmps)
                {
                    Color newColor = tmp.color;
                    newColor = new Color(1f, 1f, 1f, alpha);
                    tmp.color = newColor;
                }

            }
            );
    }


}

