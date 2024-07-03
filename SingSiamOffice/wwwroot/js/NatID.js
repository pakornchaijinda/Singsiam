
function initCardReader(page) {
   
    checkDriver(function (hasDriver, isUpdated, usingVer) {

       
                //read
                readCard(0, function (resOBJ) {
                 //   setData(resOBJ, page);

                    console.log('data', resOBJ);

                    return resOBJ;
                });
            
        

    
        

    });
}
function Read_NatID() {

    readCardThaiID(0).then(function (result) {
        console.log('Result from readCard:', result);
        jsToDotNetSamples.DotNetSays(result);
    }).catch(function (error) {
        console.error('Error from readCard:', error);
    });  


  
      
}
window.jsToDotNetSamples = {
    dotNetReference: null,
    userReference: null,
    setDotNetReference: function (dotNetReference) {
        this.dotNetReference = dotNetReference;
        //console.log(1);
    },
    setUserReference: function (userReference) {
        this.userReference = userReference;
    },
    printPersonFromDotNet: function () {
        this.dotNetReference.invokeMethodAsync("GetPerson", JSON.stringify(this.userReference)).then(person => { console.log(person); });
    },
    DotNetSays: function (result) {
        this.dotNetReference.invokeMethodAsync("ReadCardNatID", result);
    },
    triggerFinalQuizTime: function () {
        this.dotNetReference.invokeMethodAsync("QuizTime", "done");
    },
    triggerEndCourseTime: function () {
        this.dotNetReference.invokeMethodAsync("End_Course", "done");
    }

};
function readCardNatID(){
    readCard(0, function (resOBJ) {
        jsToDotNetSamples.DotNetSays(resOBJ);
        console.log('data', resOBJ);
     
    });
}

function checkDriver(callback) {
    resultbool = false;
    isUpdated = false;

    httpGetAsync("http://localhost:21998/info", function (resStatus, resText) {
        var version = 'none';
        console.log('resText', resText)
        if (resText !== null) {
            var resObj = JSON.parse(resText);
            if (resObj) {
                if (resObj.result === 'ok') {
                    resultbool = true;
                    //appInsights.trackEvent("Scan ID Card", { "Version": resObj.version });
                    if (resObj.version >= 0.8) {
                        isUpdated = true;
                    }
                    version = resObj.version;
                }
            }
        }
        else {
            console.log('is null');
        }
        callback(resultbool, isUpdated, version)
    });

}

function readCard(mode, callback) {
    var url = 'http://localhost:21998/readCard';
     httpGetAsync(url, function (resStatus, resText) {
         console.log('resText', resText)
        if (resText !== null) {
            var resObj = JSON.parse(resText);
            if (resObj) {
                if (resObj.result === 'ok') {
                    //  console.log('data', resObj);
                   
                    callback(resObj);
                }
            }
        }
        else {
            console.log('reader error');
         }
        
    });

}

function readCardThaiID(mode) {
    var url = 'http://localhost:21998/readCard';

    // สร้าง Promise ใหม่
    return new Promise(function (resolve, reject) {
        httpGetAsync(url, function (resStatus, resText) {
            console.log('resText', resText)
            if (resText !== null) {
                var resObj = JSON.parse(resText);
                if (resObj && resObj.result === 'ok') {
                    // เมื่อได้ข้อมูลที่ต้องการแล้วให้ resolve Promise พร้อมส่งผลลัพธ์ไปยัง callback function
                    resolve(resObj);
                } else {
                    // หากมีข้อผิดพลาดในการอ่านข้อมูล ให้ reject Promise พร้อมส่งข้อผิดพลาดไปยัง callback function
                    reject('reader error');
                }
            } else {
                // หากไม่มีข้อมูลที่ส่งกลับมา ให้ reject Promise พร้อมส่งข้อผิดพลาดไปยัง callback function
                reject('reader error');
            }
        });
    });
}
function setData(resObj, page) {
    console.log(resObj.nat_id, page)
    var obj = {
        resObj
        //natID: resObj.nat_id,
        //fname: resObj.fname_th,
        //lname: resObj.sname_th,
        //fname_en: resObj.fname_en.
        //    lname_en: resObj.sname_en,
        //gender: resObj.gender,
        //address

    };
    return obj;
 

}

function httpGetAsync(theUrl, callback) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4 && xmlHttp.status === 200) {
            callback(xmlHttp.status, xmlHttp.responseText);
        }
    };
    xmlHttp.onerror = function (ex) {
        console.log('Boom!');
        callback(xmlHttp.status, null)
    };
    xmlHttp.onload = function () {

    };
    xmlHttp.open("GET", theUrl, true);
    xmlHttp.send(null);
}

function httpPostAsync(theUrl, params, callback) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4 && xmlHttp.status === 200) {
            callback(xmlHttp.status, xmlHttp.responseText);
        }
    };
    xmlHttp.onerror = function (ex) {
        console.log('Boom!');
        callback(xmlHttp.status, null)
    };
    xmlHttp.onload = function () {

    };
    xmlHttp.open("Post", theUrl, true);
    xmlHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xmlHttp.send(params);
}

function httpPostJSONAsync(theUrl, json, callback) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4 && xmlHttp.status === 200) {
            callback(xmlHttp.status, xmlHttp.responseText);
        }
    };
    xmlHttp.onerror = function (ex) {
        console.log('Boom!');
        callback(xmlHttp.status, null)
    };
    xmlHttp.onload = function () {

    };
    xmlHttp.open("Post", theUrl, true); // true for asynchronous 
    xmlHttp.setRequestHeader("Content-Type", "application/json");
    xmlHttp.send(JSON.stringify(json));
}


