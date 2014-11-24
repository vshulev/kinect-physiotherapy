
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
        renderer.setSize(window.innerWidth, window.innerHeight);
        document.body.appendChild(renderer.domElement);
        camera.position.z = 6;
        camera.position.y = 4;

        // lighting
        scene.add(new THREE.AmbientLight(0xffffff));
        var light = new THREE.PointLight(0xffffff, 1, 100)
        light.position.set(0, 2, 5);
        scene.add(light);

        // environment
        groundTexture.wrapS = groundTexture.wrapT = THREE.RepeatWrapping; // ???
        groundTexture.repeat.set(10, 10); // ???

        var ground = new THREE.Mesh(
            new THREE.PlaneGeometry(10, 10),
            new THREE.MeshBasicMaterial({
                color: 0xffffff,
                ambient: 0x333333, // ???
                map: groundTexture
            }));
        ground.rotation.x = -Math.PI / 2;

        scene.add(ground);
    }

    function render() {
        requestAnimationFrame(render);
        model.update();
        renderer.render(scene, camera);
        controls.update();
    }
}