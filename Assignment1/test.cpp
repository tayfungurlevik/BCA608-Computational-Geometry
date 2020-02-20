#include <GL/glew.h>
#include <GL/freeglut.h> 
#include <glm.hpp>
#include <vector>
using namespace std;

struct Segment
{
	glm::vec3 a;
	glm::vec3 b;

};
struct Cokgen
{
	vector<Segment> segments;
};
Cokgen p1;
// Drawing routine.
void drawScene(void)
{
	glClear(GL_COLOR_BUFFER_BIT);

	glColor3f(1, 0.0, 0.0);

	// Draw a polygon with specified vertices.
	glBegin(GL_TRIANGLES);
	glVertex3f(p1.segments.at(0).a.x, p1.segments.at(0).a.y, p1.segments.at(0).a.z);
	glVertex3f(p1.segments.at(0).b.x, p1.segments.at(0).b.y, p1.segments.at(0).b.z);
	glVertex3f(p1.segments.at(1).a.x, p1.segments.at(1).a.y, p1.segments.at(1).a.z);
	glVertex3f(p1.segments.at(1).b.x, p1.segments.at(1).b.y, p1.segments.at(1).b.z);
	glVertex3f(p1.segments.at(2).a.x, p1.segments.at(2).a.y, p1.segments.at(2).a.z);
	glVertex3f(p1.segments.at(2).b.x, p1.segments.at(2).b.y, p1.segments.at(2).b.z);
	glEnd();

	glFlush();
}

// Initialization routine.
void setup(void)
{
	glClearColor(1.0, 1.0, 1.0, 0.0);
	
	Segment s1;
	glm::vec3 v1;
	v1.x = 5;
	v1.y = 15;
	v1.z = 0;
	s1.a = v1;
	glm::vec3 v2;
	v2.x = 70;
	v2.y = 10;
	v2.z = 0;
	s1.b = v2;
	p1.segments.push_back(s1);
	Segment s2;
	glm::vec3 v3;
	v3.x = 40;
	v3.y = 30;
	v3.z = 0;
	s2.a = v3;
	glm::vec3 v4;
	v4.x = 30;
	v4.y = 20;
	v4.z = 0;
	s2.b = v4;
	p1.segments.push_back(s2);
	Segment s3;
	glm::vec3 v5;
	v5.x = 90;
	v5.y = 10;
	v5.z = 0;
	s3.a = v5;
	glm::vec3 v6;
	v6.x = 50;
	v6.y = 50;
	v6.z = 0;
	s3.b = v6;
	p1.segments.push_back(s3);
	
	
}

// OpenGL window reshape routine.
void resize(int w, int h)
{
	glViewport(0, 0, w, h);

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0, 100.0, 0, 100.0, -1.0, 1.0);

	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
}

// Keyboard input processing routine.
void keyInput(unsigned char key, int x, int y)
{
	switch (key)
	{
	case 27:
		exit(0);
		break;
	default:
		break;
	}
}

// Main routine.
int main(int argc, char** argv)
{
	glutInit(&argc, argv);

	glutInitContextVersion(4, 3);
	glutInitContextProfile(GLUT_COMPATIBILITY_PROFILE);

	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGBA);

	glutInitWindowSize(500, 500);
	glutInitWindowPosition(100, 100);

	glutCreateWindow("square.cpp");

	glutDisplayFunc(drawScene);
	glutReshapeFunc(resize);
	glutKeyboardFunc(keyInput);

	glewExperimental = GL_TRUE;
	glewInit();

	setup();

	glutMainLoop();
}