
Exercise = function (sets, reps, b1, b2, sa, ea) {

    var DA = 0.35; // == 20 degrees;

    var totalSets = sets;
    var totalReps = reps;

    var currentSets = 0;
    var currentReps = 0;

    /* type BoneType */
    var bone1 = b1;
    var bone2 = b2;

    var startAngle = sa;
    var endAngle = ea;

    var onStartFn = function () { };
    var onRepFn = function () { };
    var onSetFn = function () { };
    var onCompleteFn = function () { };

    var isStarted = false;
    var isCompleted = false;
    var midRep = false; // whether user is in a middle of rep or not

    this.update = function (bones) {
        if (isCompleted) {
            return;
        }

        var b1dv = bones[bone1].getDirectionVector().clone();
        var b2dv = bones[bone2].getDirectionVector().clone().negate();

        var angle = b1dv.angleTo(b2dv);

        // check if should start exercise
        if (!isStarted && Math.abs(angle - startAngle) <= DA) {
            isStarted = true;
            onStartFn();
            return;
        }

        if (!midRep && Math.abs(angle - endAngle) <= DA) {
            midRep = true;
            return;
        }

        if (midRep && Math.abs(angle - startAngle) <= DA) {
            midRep = false;
            currentReps++;
            onRepFn();

            if (currentReps == totalReps) {
                currentReps = 0;
                currentSets++;
                onSetFn();

                if (currentSets == totalSets) {
                    isCompleted = true;
                    onCompleteFn();
                }
            }
        }
    }

    this.getRepCount = function() {
        return currentReps;
    }

    this.getSetCount = function () {
        return currentSets;
    }

    this.onStart = function (callback) {
        onStartFn = callback;
    }

    this.onRepDone = function (callback) {
        onRepFn = callback;
    }

    this.onSetDone = function (callback) {
        onSetFn = callback;
    }

    this.onComplete = function(callback) {
        onCompleteFn = callback;
    }

}