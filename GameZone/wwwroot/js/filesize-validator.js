$.validator.addMethod("filesize", function (value, elem, param) {
    return this.optional(elem) || elem.files[0].size <= param;
})