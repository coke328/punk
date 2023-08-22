using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*대화할 오브젝트에 삽입할 스크립트 고유 id와 상호대화여부 확인을 위해 존재
 XML 적용했을때 제거하고 XML로 전부 대체 가능한지 여부 불투명*/
public class Obj_Dialog_Data : MonoBehaviour
{
    public int Id;
    public bool isTalk;
}
