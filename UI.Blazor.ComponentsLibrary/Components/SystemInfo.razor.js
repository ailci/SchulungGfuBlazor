export function GetBrowserInfo(dotNetObjRef, methodName) {
    let info = new Map();
    info.set("Uhrzeit", new Date().toLocaleTimeString());
    info.set("Browser Type", navigator.appCodeName);
    info.set("Browser Version", navigator.appVersion);

    const plainObject = Object.fromEntries(info); // muss man in objcet umwandeln, weil Map nicht direkt übergeben werden kann

    dotNetObjRef.invokeMethodAsync(methodName, plainObject);
    //dotNetObjRef.invokeMethodAsync(methodName, Array.from(info));

    console.log("PlainObject", plainObject);
    console.table("Info", info);
}