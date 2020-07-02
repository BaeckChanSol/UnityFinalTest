본인의 정보
 - 이름 : 백찬솔
 - 학번 : 2016184020
 - 학과 : 엔터테인먼트컴퓨팅
 - 엔진 버전 : 2019.3.7f1 Personal

(본 내용은 txt로 확장자를 변환하여 보는 것을 추천합니다.)

게임 소개 
 - 졸업 작품의 알파버전으로서 간단하게 제작한 PVE(보스) 형태의 3인칭 백뷰 액션 게임입니다.
 - 보스와 플레이어 모두 특정 행동을 할 때 스태미나를 소모하며 스태미나가 부족하면 사용할 수 없습니다.
 - 플레이어는 스태미나를 소모하지 않으면 자동으로 회복되지면 보스는 전부 사용하고 일정 휴식 기간을 가져야만 스태미나를 회복할 수 있습니다.
 - 게임은 느낌을 파악하기 위한 알파버전 임으로 유니티의 시작 버튼을 누르면 자동으로 전투 상황이 시작됩니다.
 - 플레이어의 체력 혹은 보스의 체력이 0가 되면 게임이 끝나며 enter를 눌러 다시 시작할 수 있습니다.

조작키
   W, A, S, D : 상 / 하 / 좌 / 우 이동
   마우스 L클릭 : 공격 (최대 3타, 1번 누를때마다 1타)
   마우스 R클릭 : 보호막 가드 (피격 시 스태미나 1소모)
   Space  : 회피 (사용 시 1스태미나 소모)
   Shift (press) : 뛰기 (사용 시 초당 스태미나 소모)
   Q : 스킬 창 열기 (적이 회복 단계면 슬로우 모션 적용 / 적이 회복 단계에서만 스킬 잠금 해제되며 기본 상태일 때에는 스킬창은 열리나 자물쇠로 스킬이 잠겨져 있는 상태) [스킬창이 열려있을 때에는 카메라 회전 불가)
	Q창을 열고 마우스를 해당 스킬이 있는 방향으로 움직이면 선택가능
	선택(외각에 네온 효과)된 스킬을 마우스L클릭으로 사용 가능
	구현 스킬
	- 1 스킬 : 원형 폭발진 설치 

개발 에셋 설명 ( '-' : 폴더명 / '->' : 파일) [프로젝트 폴더 기준]
	- Animation : AssetStore에 파일이 있어 별도의 에셋으로 묶지 않은 애니메이션
		-> Golem : AssetStore에서 제공하는 골렘 모델과 애니메이션을 활용하여 직접 제작한 애니메이션 컨트롤러
	- AssetStore : 최소한의 가공(호환)을 한 파일 모음
		- AllSkyFree : AssetStore에서 다운로드 받은 Skybox
		- Hovi Studio : AssetStore에서 다운로드 받은 Particle (스킬 1에 사용)
		- Mini Legion Rock Golem PBR... : AssetStore에서 다운로드 받은 골렘 모델링, 텍스처 및 애니메이션
		- MotionTrail : 유튜브에서 다운로드 받은 오픈 소스 (출처 : https://www.youtube.com/watch?v=bKgEW8Jy4l0&t=3s) 
			[자식 객체는 처리해주지 않는 object poller와 호환되도록 스크립트 약간 수정] 
		- Seven Swords : AssetStore에서 다운로드 받은 검 모델링 및 텍스처
	- DataBase : Scriptable Object
		-> EventData : EventHandler에서 사용하는 Scriptable Object, 데이터 베이스에 있는 이벤트만 등록할 수 있다.
	- Image : 본인이 직접 제작한 Image, 주로 UI 관련
		- HP : 체력 UI 관련 이미지
		- SP : 스태미나 UI 관련 이미지
		- StateShow : 결과창 (승리, 패배) 이미지
		- Skill : 스킬 창 및 스킬 이미지
			스킬 아이콘 출처 : https://www.pinterest.co.kr/pin/540783867758345257/
			잠물쇠 아이콘 출처 : https://www.dreamstime.com/chain-thick-line-icon-outline-vector-sign-chain-thick-line-icon-outline-vector-sign-white-background-image124505287
	- Material : Shader Graph로 제작한 shader 및 유니티에서 제공하는 기본 형식을 사용해 만든 Material 
		-> Red / Blue / Green / Yellow Glow : 기본 URP 쉐이더가 제공하는 material 형식을 사용하여 Emission을 통해 빛나게 만든 단색 Material
		-> Body : 기본 URP 쉐이더가 제공하는 material 형식을 사용하여 만든 플레이어 캐릭터 Material 
		-> Roll : ShaderGraph를 통해 제작한 Shader를 적용, 잔상을 위한 Material
		-> Shield : ShaderGraph를 통해 제작한 Shader를 적용, 보호막을 위한 Material
		-> Elect : ShaderGraph를 통해 제작한 Shader를 적용. 골렘이 공격을 할 때 생성되는 파티클을 위한 Material
	- Player : Player와 관련된 파일 (어느 정도의 가공이 있었기에 별도 파일로 구분)
		- Body : Mixamo 에서 제공한 기본 모델링 prefabs
		- Animation : Mixamo 에서 제공한 애니메이션 모델링에서 추출한 애니메이션과 컨트롤러 (컨트롤러는 본인이 제작)
		출처 : https://www.mixamo.com/#/?page=1&query=walk
	- Prefabs : ObjectPoller를 사용하여 생성하기 위한 prefabs
		- ElectParticle : 보스 몬스터가 공격할 때 생기는 전기 파티클 (참고 : https://www.youtube.com/watch?v=u9lOaPVtSqg&t=480s)
		- MotionTrail : 본 프로젝트에 사용할 수 있도록 호환을 맞춘 MotionTrail Prefabs
		- ResultUI : 게임 결과 창 UI 
		- RotatorPS1 : 본 프로젝트에 사용할 수 있도록 스크립트 추가 및 호환을 맞춘 Hovi Studio Prefabs
	- ProBulider Data : ProBulider 사용으로 인한 자동 생성 파일
	- Scenes : 기본 폴더
	- Scripts : 직접 제작한 스크립트 모음 [AssetStore에서 다운 받은 prefabs에 적용하기도 함]
		- Blade : Seven Sword의 검 오브젝트에 들어가는 스크립트, 플레이어가 가지고 있는 검이 공격 중일 때 적과 충돌 중인지 검사
		- Elect : RotatorPS1에 들어가는 스크립트, 폭발하는 시점에 맞춰서 범위 내 몬스터에게 데미지
		- EventCreater : 시작 시에 EventHandler에 이벤트를 등록하는 스크립트
		- EventDatabase : Scriptable Object - 이벤트 관련 데이터 저장
		- EventForm : EventDatabase에 사용되는 데이터 양식
		- EventHandler : 다른 스크립트의 요청에 따라 이벤트를 등록, 관리, 시행하는 핸들러
		- GolemController : 골렘의 행동을 컨트롤 하는 스크립트 (쿨타임 공격, 이동, 회전 등)
		- GolemStatus : 골렘의 체력, 스태미나, 동작 상태를 관리하는 스크립트, 다른 스크립트 요청에 따른 상태 전달 및 애니메이션 중 실행되는 함수 등을 관리
		- healthVolume : Global Volume 오브젝트에 들어가는 스크립트, 플레이어의 체력 상태를 받아 위험 상태일 경우 Vignette를 통해 화면 붉은 외각 효과
		- ObjectPoller : 등록한 prefabs를 생성, 생성되어있는지 확인, 제거[Setative(false)를 통한 비활성화]하는 스크립트
		- PlayerControler : Player의 자식 객체 Body에 들어가는 스크립트, update를 통한 키입력과 fixedupdate에 playerstatus에 상태 변경 명령 전달 및 transform을 사용하여 이동
		- PlayerStatus : 플레이어의 체력, 스태미나, 동작 상태를 관리하는 스크립트, 다른 스크립트 요청에 따른 상태 전달 및 애니메이션 중 실행되는 함수 등을 관리
		- ResultPrint : ResultUI (prefabs)에 들어가는 스크립트, 승리, 패배 결과창 중 하나만 보여주는 함수, enter 키입력 시 게임 재시작 등의 기능 포함
		- rock : 골렘이 던지는 rock (prefabs)에 들어가는 스크립트, 던지는 돌과 플레이어의 충돌 검사 및 데미지
		- Rotate / Scale: Rotate의 자식 객체에 들어가는 스크립트, 실시간으로 특정 위치 기준 회전, 자기 자신 회전 및 스케일 변경
		- skill : RotatorPS1에 들어가는 스크립트, 폭팔 시점에 맞춰 범위 내에 있는 적들에게 데미지 부여
		- skillManger : SkillUI에 들어가는 스크립트, 스킬 ui창이 활성화되었을 때의 처리, 스킬을 선택했을 때의 처리를 정리
		- StatusInterface : PlayerStatus와 GolemStatus의 부모 스크립트, 플레이어, 보스 등 생명체에 공통적으로 들어가는 수치 , 레퍼런스, 처리 포함
		- Sword : sword에 들어가는 스크립트, 플레이어가 공격 중 일 때 검 Collider와 충돌한 적에게 데미지
		- UI : UI에 들어가는 스크립트 기본적인 보스, 플레이어의 체력, 스태미나에 따른 UI 출력 변화에 관한 스크립트
	- Shaders : Shader Graph를 사용하여 직접 제작한 쉐이더
		-> AttackForce : 골렘이 공격할 때 만들어지는 전기 파티클 unlit shader
		-> Elect : 골렘이 공격할 때 만들어지는 전기 파티클 PBR shader
			(출처 : https://www.youtube.com/watch?v=u9lOaPVtSqg&t=480s )
		-> Glow Force : 쉴드, 회피 시 잔상 생성에 사용 되는 PBR shader
			(출처 : https://www.youtube.com/watch?v=3ALkvh3pJXQ / https://www.youtube.com/watch?v=NiOGWZXBg4Y )
	- Texture : 본인이 직접 제작한 텍스처
		-> Shield : Glow Force에 사용되는 텍스처, 쉴드는 이 텍스처를 적용했고 회피 잔상은 텍스처 없이 만든 material 이다.

외부 에셋 정리
	- AllSkyFree : AssetStore에서 다운로드 받은 Skybox
	- Hovi Studio : AssetStore에서 다운로드 받은 Particle (스킬 1에 사용)
	- Mini Legion Rock Golem PBR... : AssetStore에서 다운로드 받은 골렘 모델링, 텍스처 및 애니메이션
	- Seven Swords : AssetStore에서 다운로드 받은 검 모델링 및 텍스처
	- MotionTrail : 유튜브에서 다운로드 받은 오픈 소스 (출처 : https://www.youtube.com/watch?v=bKgEW8Jy4l0&t=3s) 
	- Player 모델링 및 애니메이션 : mixamo 사이트에서 다운로드 받은 오픈소스 (출처 : https://www.mixamo.com/#/?page=1&query=walk)
	- UI 스프라이트 일부
		스킬 아이콘 출처 : https://www.pinterest.co.kr/pin/540783867758345257/
		잠물쇠 아이콘 출처 : https://www.dreamstime.com/chain-thick-line-icon-outline-vector-sign-chain-thick-line-icon-outline-vector-sign-white-background-image124505287

적용된 기술 (외부 Asset 및 기본 설정 제외)
	- 수업 내용
		Collider (3주차)
		UI Canvas (3주차 / 10 주차) : text, image, slider 사용 /  Rect Transform(Ancher등) 설정
		Particle (3주차)
		Material (3주차)
		Class Field, Property, Inheritance, 접근 제한자 등 (6주차)
		Time.deltatime (6주차) [스태미나 등 시간 당 처리]
		GetComponet<>() (6주차)
		Mathf (6주차) Lerp
		게임 모델 : 무한 상태 머신 (7주차)
		코루틴 (7주차) / 비고 : 메인으로 쓰지 않고 부가적인 처리에 휴식 시간을 체크하는 부가적인 기능에 코루틴 사용 (playerstatus - 스태미나 회복 / 뛰기를 통한 스태미나 소모 / 행동 후 휴식 상태 전환에 사용)
		이벤트 헨들링 (7주차 / 12주차 / 13주차)
		transform position(캐릭터 이동), scale, rotate(게임 오브젝트 rotate) 및 Vector.Distance (레이 사정거리 검사) / Quaternion(캐릭터, 카메라 회전) (9주차)
		Rigdybody(필드와 같은 배경 요소 제외 전반적인 오브젝트에 적용), addForce (rock 던지기) (9주차)
		input (플레이어 컨트롤러 제작) (9주차)
		ProGrid / Probuilder(필드 제작) (11주차)
		URP (11주차)
		라이트 베이킹 (Field 및 LightBoardGroup에 적용) (11주차)
		라이트 프로브 (11주차)
		ObjectPoller (12주차)
		Scriptable Object (13주차)
		애니메이션 컨트롤러 		
		[SerializeField] / [range] - Rotate 자식 객체 Roate Center Speed
	- 수업 외
		SceneManager : eventcreater에서 재시작을 위해 사용
		Raycast : 플레이어 벽 충돌 전에 몸 일부가 벽을 돌파하지 않고 앞에 짧은 RAY를 통하여 장애물이 있으면 이동 못하도록 제작 
		AnimationEvent : 애니메이션에서 추가해 해당 애니메이션이 있는 스크립트의 함수를 호출할 수 있는 기능 / 보스 돌던지기, 플레이어 3타 분할(클릭한 만큼 공격, 최대 3타) 등을 이를 통하여 구현 
		ShaderGraph
		URP GlobalVolume : Bloom / Color Curves / Shadows Midtones Highlights / Vigntte 적용 및 Vigntte 스크립트를 통한 라이브 수치 변경

비고
 본 프로젝트는 시간이 부족한 관계로 여러 주차에 걸쳐서 차근차근 만든 것이라 몇몇 스크립트가 제작 시기에 따라 방식이 다르게 구성되었습니다. 가장 빨리 만들어진 스크립트일수록 c#의 객체지향을 살리지 못하고 있습니다.
 해당 프로젝트에서 구현, 제작하겠다는 내용은 모두 구현했습니다 
(시간요소 고려 후 추가하겠다는 내용은 제작하지 않았습니다. 말 그대로 여유가 있으면 추가하겠다는 내용으로 실제 추가가 되지 않을 수 있다는 것을 이미 나타내었기에 큰 문제가 없다고 생각하고 있습니다.)