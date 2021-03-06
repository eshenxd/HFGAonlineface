#include "create_new_thread.h"
#include "get_cnn_feature.h"
#include "register.h"
#include "face_compare.h"
#include "show_compare_result.h"

using namespace std;

void register_operate(Main* job);
void recognition_operate(Main* job);

void create_new_thread(Main* job)
{
	_beginthread(feature_operate,NULL,(void*)job);
}

void __cdecl feature_operate(void* pThis)
{
	Main* job=(Main*)pThis;

	

	while(true)
	{
		WaitForSingleObject(job->g_event,INFINITE);

		

		get_cnn_feature(job->image_Face,job->feature);
		if(job->mode_index==1)
			register_operate(job);

		else
			recognition_operate(job);	
	}

}

void register_operate(Main* job)
{	
	Register reg;
	reg.register_init();

	cout<<"\n";
	cout<<"注册人信息：\n";
	cout<<"/*"<<endl;
	cout<<"姓      名："+job->people.name<<endl;
	cout<<"身份证编号："+job->people.cardNum<<endl;
	cout<<"*/"<<endl;
	
	People people;

	

	people.cardNum=job->people.cardNum;
	people.feature=job->feature;
	people.image=job->image_face_tmp;
	people.name=job->people.name;
	people.card_pic=job->people.card_pic;

	reg.save_register_info(people);

	job->register_OK=true;
}

void recognition_operate(Main* job)
{
	Register reg;
	reg.register_init();//这个初始化在注册人数变多时，需要放在循环之外，只读文件一次，以提供速度

	People people;//存放比对结果

	bool flag=reg.load_register_info(job->Data);
	if(flag==false)return;

	double mindistance=face_compare(*job,people);

	cout<<mindistance<<endl;

	ShowResult show(people);
	show.show_result();

	job->Compare_OK=true;
}