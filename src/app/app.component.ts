import { Component } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

@Component({

    selector: 'app-root',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']

})

export class AppComponent {

    origin= '';
    destination='';
    trafficData: any= null;
    loading= false;
    arrivalTime='7:00'
    errorMessage = '';


    constructor(private http: HttpClient){}

    getTraffic(event: Event){
        event.preventDefault();

        console.log("Calling API with:", this.origin, this.destination);

        this.loading=true;
        this.errorMessage= '';
        this.trafficData=null;

      const url = `https://localhost:7007/api/traffic/smart-departure` +
    `?origin=${encodeURIComponent(this.origin)}` +
    `&destination=${encodeURIComponent(this.destination)}` +
    `&arrivalTime=${this.arrivalTime}`;



        this.http.get(url).subscribe({
            next: (data) => {
                this.trafficData=data;
                this.loading=false;
            },
            error: () => {
                this.errorMessage = 'Failed to recieve Traffic Data'
                this.loading= false;
            }
        });
    }
}