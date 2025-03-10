using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    //�A�j���[�V�������邽�߂̃R���|�[�l���g������
    private Animator myAnimator;

    //Unity�������ړ�������R���|�[�l���g������(�O�i�P)
    private Rigidbody myRigidbody;

    //�O�����̑��x(�O�i�P)
    private float velocityZ = 16f;

    //�������̑��x�i�ǉ��j
    private float velocityX = 10f;

    //������̑��x�i�W�����v�Ɋւ��鎖�j
    private float velocityY = 10f;

    //���E�̈ړ��ł���͈́i�ǉ��j
    private float movableRange = 3.4f;

    // Start is called before the first frame update
    void Start()
    {
        //Animator�R���|�[�l���g���擾
        this.myAnimator = GetComponent<Animator>();

        //����A�j���[�V�������J�n  ("Speed", 0.2)����;
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbody�R���|�[�l���g���擾�i�O�i�P�j
        this.myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //�������̓��͂ɂ�鑬�x�i�ǉ��j
        float inputVelocityX = 0;

        //������̓��͂ɂ�鑬�x�i�W�����v�Ɋւ��鎖�j
        float inputVelocityY = 0;

        //Unity��������L�[�܂��̓{�^���ɉ����č��E�Ɉړ�������i�ǉ��j
        if (Input.GetKey(KeyCode.LeftArrow) && -this.movableRange < this.transform.position.x)
        {
            //�������ւ̑��x�����i�ǉ��j
            inputVelocityX = -this.velocityX;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.movableRange)
        {
            //�E�����ւ̑��x�����i�ǉ��j
            inputVelocityX = this.velocityX;
        }

        //�W�����v���Ă��Ȃ����ɃX�y�[�X�������ꂽ��W�����v����i�W�����v�Ɋւ��鎖�j
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //�W�����v�A�j�����Đ��i�W�����v�Ɋւ��鎖�j
            this.myAnimator.SetBool("Jump", true);
            //������ւ̑��x�����i�W�����v�Ɋւ��鎖�j
            inputVelocityY = this.velocityY;
        }
        else
        {
            //���݂�Y���̑��x�����i�W�����v�Ɋւ��鎖�j
            inputVelocityY = this.myRigidbody.velocity.y;
        }
        //Jump�X�e�[�g�̏ꍇ��Jump��false���Z�b�g����i�W�����v�Ɋւ��鎖�j
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
        //Unity�����ɑ��x��^����i�O�i�P�j
        //x�v�f�ɐ���������ƍ��E�΂߁A���v�f�ɓ����Ə㉺�ɐi
        //(�ύX����inputVelocityX�����)
        //�W�����v�Ɋւ��鎖(inputVelocityY)
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);
    }
}
