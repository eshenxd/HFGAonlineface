#include "header.h"
#include "version_info_show.h"

using namespace std;
using namespace cv;




int main( int argc, char* argv[])
{
	//version_info_show("�й��ƴ�������������ϵͳv1.0","����ע�������","���紫������ư���ʡ�ص�ʵ����","2015��4��10��");

	//cvNamedWindow("VIDEO", CV_WINDOW_AUTOSIZE);
	//
	////ѡ�����ģʽ

	//cout<<"��ѡ��������ܣ�[1/2]\n";
	//cout<<"               1.Register ע��\n";
	//cout<<"               2.Recognition ʶ��\n";
	argc = 2;
	argv[1] = "2";

	if(argc != 2)
		{
			cout<<"ERROR.."<<endl;
			return -1;
	    }

	string function_num;
	function_num = argv[1];

	//cout<<function_num<<endl;
	//system("pause");

	if(function_num == "1")
	{
		cout<<"��ѡ����ע��ģʽ!"<<endl;
		cout<<"��ʼע�����.."<<endl;
		register_mode();
	}

	else if(function_num == "2")
	{
		//cvNamedWindow("CARD",CV_WINDOW_AUTOSIZE);
		cout<<"��ѡ����ʶ��ģʽ��"<<endl;
		cout<<"��ʼʶ�����.."<<endl;

		recognition_mode();
	}

	else
	{
		cout<<"��������룬������(1/2)"<<endl;
		cout<<"1:ע��"<<endl;
		cout<<"2:ʶ��"<<endl;
	}	

	
		
			
	
}