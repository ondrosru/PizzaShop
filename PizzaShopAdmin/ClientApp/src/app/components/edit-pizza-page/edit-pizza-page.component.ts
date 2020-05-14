import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-edit-pizza-page',
  templateUrl: './edit-pizza-page.component.html',
  styleUrls: ['./edit-pizza-page.component.css']
})
export class EditPizzaPageComponent implements OnInit {
  selectedFile: File = null;
  url: string;

  constructor() { }

  ngOnInit(): void {
  }



  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }

  onUpload() {
    const fd = new FormData();
    const reader = new FileReader();
    reader.readAsDataURL(this.selectedFile);

    reader.onload = (event) => {
      this.url = event.target.result as string;
    };
    // https://developer.mozilla.org/ru/docs/Web/API/FormData/append
    fd.append('image', this.selectedFile, this.selectedFile.name);
    console.log(this.selectedFile);

    // this.http.post('./api/test-api-for-upload', fd)
    //   .subscribe(res => {
    //     console.log('res: ', res);
    //   });


    // II
    // если ваш сервер поддерживает прием бинарных файлов, то вы можете отправить файл следующим образом:
    // this.http.post('./api/test-api-for-upload', this.selectedFile)

  }

}
