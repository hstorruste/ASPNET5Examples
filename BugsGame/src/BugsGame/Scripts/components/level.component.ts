import {Component,
    OnInit} from 'angular2/core';
import {Level} from "../models/Level.model";

@Component({
    selector: "level",
    template: `
        <h1>{{level.title}}</h1>
        <p>Time left: <b>{{level.seconds}}</b></p>
        
    `,
})
export class LevelComponent implements OnInit {
    public level: Level = {
        id : 1,
        background : "",
        title: "Level 1",
        seconds: 60
    };
    private timeStarted = false;

    ngOnInit() {
        this.startTime();
    }
    
    startTime() {
        if (!this.timeStarted) {
            var clock = setInterval(() => {
                this.level.seconds--;
                if (this.level.seconds == 0) {
                    clearInterval(clock);
                }
            }, 1000);
            this.timeStarted = true;
        }
    }
}

