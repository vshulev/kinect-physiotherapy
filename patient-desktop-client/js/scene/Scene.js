
// TODO get rid of magic numbers
function Scene() {

    /* The THREE.Scene */
    var scene = new THREE.Scene();

    /* camera */
    var camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);

    /* renderer */
    var renderer = new THREE.WebGLRenderer();

    /* The mouse movement controls */
    var controls = new THREE.OrbitControls(camera, renderer.domElement);

    /* the skinned character model */
    var model = new Model(scene, camera);

    /* the texture for the plane the model stands on */
    var groundTexture = THREE.ImageUtils.loadTexture("textures/wood.png");

    setUp();

    this.beginRendering = function () {
        render();
    }

    function setUp() {
        // camera and rendering
        setUpRenderer();
        setUpCamera();
        setUpLight();
        scene.fog = new THREE.Fog(0x000000, 0.00015, 100);
        setUpGround();

        // environment
        groundTexture.wrapS = groundTexture.wrapT = THREE.RepeatWrapping; // ???
        groundTexture.repeat.set(1000, 1000); // ???
    }

    function render() {
        requestAnimationFrame(render);
        model.update();
        renderer.render(scene, camera);
        controls.update();
    }

    function setUpRenderer() {
        renderer.setSize(window.innerWidth, window.innerHeight);
        renderer.shadowMapEnabled = true;
        document.body.appendChild(renderer.domElement);
    }

    function setUpCamera() {
        camera.position.z = 6;
        camera.position.y = 4;
    }

    function setUpLight() {
        scene.add(new THREE.AmbientLight(0xffffff));
        //var light = new THREE.SpotLight(0xffffff, 2, 100, 0.754, 2.66);
        var light = new THREE.SpotLight(0xffffff);
        light.position.set(0, 1500, 0);
        light.castShadow = true;
        light.shadowMapWidth = 1024;
        light.shadowMapHeight = 1024;
        light.shadowCameraNear = 500;
        light.shadowCameraFar = 4000;
        light.shadowCameraFov = 30;
        scene.add(light);
    }

    function setUpGround() {
        var ground = new THREE.Mesh(
            new THREE.PlaneGeometry(1000, 1000),
            new THREE.MeshLambertMaterial({
                color: 0xe0e0e0,
                ambient: 0x333333, // ???
                map: groundTexture
            }));
        ground.rotation.x = -Math.PI / 2;
        ground.receiveShadow = true;

        scene.add(ground);
    }
}